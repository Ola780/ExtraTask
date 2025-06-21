namespace ZadanieDodatkowe.DTOs;

public class ParticipantReportDto
{
    public int eventId { get; set; }
    public string eventTitle { get; set; }
    public DateTime eventDate { get; set; }
    public List<string> speakerSurnames { get; set; }
}