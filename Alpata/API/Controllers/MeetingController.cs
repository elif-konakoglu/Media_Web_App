using Alpata.API.Business.Interfaces;
using Alpata.Entity;
using Alpata.Model.Meeting;
using Microsoft.AspNetCore.Mvc;

namespace Alpata.API.Controllers;

[ApiController]
[Route("meeting")]
public class MeetingController : Controller
{
    private readonly IMeetingService _meetingService;
    private readonly IMailHelper _mailHelper;

    public MeetingController(IMeetingService meetingService,IMailHelper mailHelper)
    {
        _meetingService = meetingService;
        _mailHelper = mailHelper;
    }

    [HttpPost("create")]
    [ProducesDefaultResponseType(typeof(Meeting))]
    public Meeting Create([FromForm] SaveMeetingRequestDto CreateMeetingRequestDto)
    {
        Meeting meeting = _meetingService.Create(CreateMeetingRequestDto);

        _mailHelper.SendMeetingMail(meeting);

        return meeting;
    }

    [HttpGet("get/{meetingId}")]
    [ProducesDefaultResponseType(typeof(Meeting))]
    public Meeting Get(int meetingId)
    {
        return _meetingService.Get(meetingId);
    }

    [HttpGet("getAll/{userId}")]
    [ProducesDefaultResponseType(typeof(Meeting))]
    public IEnumerable<Meeting> GetAll(int userId)
    {
        return _meetingService.GetAll(userId);
    }

    [HttpDelete("delete/{meetingId}")]
    [ProducesDefaultResponseType(typeof(Meeting))]
    public Meeting Delete(int meetingId)
    {
        return _meetingService.Delete(meetingId);
    }

    [HttpPost("update")]
    [ProducesDefaultResponseType(typeof(Meeting))]
    public Meeting Update([FromForm] SaveMeetingRequestDto updateMeetingRequestDto)
    {
        return _meetingService.Update(updateMeetingRequestDto);
    }
}
