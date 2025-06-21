namespace ZadanieDodatkowe.DTOs;

public class EventGetDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }

    public List<string> Speakers { get; set; }
    public int RegisteredParticipants { get; set; }
    public int FreePlaces { get; set; }
}