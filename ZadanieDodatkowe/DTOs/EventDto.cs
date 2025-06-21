namespace ZadanieDodatkowe.DTOs;

public class EventDto
{
    public   int      Id { get; set; }
    public   string   Title { get; set; }
    public   string   Description { get; set; }
    public   DateTime Date { get; set; }
    public   int      MaxNumberParticipants { get; set; }
}