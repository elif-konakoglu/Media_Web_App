using Alpata.Entity;
using Alpata.Model.Meeting;

namespace Alpata.API.Business.Interfaces;

public interface IMeetingService
{
    public Meeting Create(SaveMeetingRequestDto createMeetingRequestDto);
    public Meeting Get(int meetingId);
    public IEnumerable<Meeting> GetAll(int userId);
    public Meeting Update(SaveMeetingRequestDto updateMeetingRequestDto);
    public Meeting Delete(int meetingId);
}