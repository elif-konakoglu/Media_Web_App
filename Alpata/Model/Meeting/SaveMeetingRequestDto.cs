﻿namespace Alpata.Model.Meeting;

public class SaveMeetingRequestDto
{
    public int? Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; }
    public IFormFile Document { get; set; }
}

