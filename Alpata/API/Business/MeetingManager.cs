using Alpata.API.Business.Interfaces;
using Alpata.Entity;
using Alpata.Model.Meeting;
using Microsoft.EntityFrameworkCore;

namespace Alpata.API.Business;

public class MeetingManager : IMeetingService
{
    private readonly AlpataAPIDbContext _dbContext;
    private readonly IFileHelper _fileHelper;


    public MeetingManager(AlpataAPIDbContext dbContext, IFileHelper fileHelper)
	{
        _dbContext = dbContext;
        _fileHelper = fileHelper;
	}

    public Meeting Create(SaveMeetingRequestDto createMeetingRequestDto)
    {
        var user = _dbContext.Users.Find(createMeetingRequestDto.UserId);

        if(user == null)
        {
            throw new ArgumentNullException("User has not found.");
        }

        Meeting entity = new Meeting
        {
            UserId = createMeetingRequestDto.UserId,
            User = user,
            Name = createMeetingRequestDto.Name,
            Description = createMeetingRequestDto.Description,
            StartDate = createMeetingRequestDto.StartDate,
            EndDate = createMeetingRequestDto.EndDate,
            Document = _fileHelper.UploadFile(createMeetingRequestDto.Document)
        };

        _dbContext.Add(entity);
        _dbContext.SaveChanges();

        return entity;
    }

    public Meeting Delete(int meetingId)
    {
        Meeting entity = _dbContext
            .Meetings
            .Find(meetingId);

        if(entity == null)
        {
            throw new ArgumentNullException("Meeting has not found.");
        }

        _dbContext.Remove(entity);
        _dbContext.SaveChanges();

        return entity;
    }

    public Meeting Get(int meetingId)
    {
        Meeting entity = FindMeetingById(meetingId);

        if (entity == null)
        {
            throw new ArgumentNullException("Meeting has not found.");
        }

        return entity;
    }

    public IEnumerable<Meeting> GetAll(int userId)
    {
        IEnumerable<Meeting> entity = _dbContext
            .Meetings
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .ToList();

        return entity;
    }

    public Meeting Update(SaveMeetingRequestDto updateMeetingRequestDto)
    {
        Meeting entity = FindMeetingById(updateMeetingRequestDto.Id);

        if (entity == null)
        {
            throw new ArgumentNullException("Meeting has not found.");
        }

        var filePath = _fileHelper.UploadFile(updateMeetingRequestDto.Document);

        entity.Name = updateMeetingRequestDto.Name;
        entity.StartDate = updateMeetingRequestDto.StartDate;
        entity.EndDate = updateMeetingRequestDto.EndDate;
        entity.Description = updateMeetingRequestDto.Description;
        entity.Document = filePath;

        _dbContext.Update(entity);
        _dbContext.SaveChanges();

        return entity;
    }

    public Meeting FindMeetingById(int? meetingId)
    {
        return _dbContext
            .Meetings
            .Find(meetingId);
    }
}
