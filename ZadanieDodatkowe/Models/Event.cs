using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZadanieDodatkowe.Models;

[Table("Event")]
public class Event
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)] 
    public string Title { get; set; } = null!;
    [MaxLength(50)]
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }
    [Required]
    public int MaxNumberParticipants { get; set; }
    
    public virtual ICollection<EventSpeaker> EventSpeakers { get; set; } = null!;
    public virtual ICollection<EventParticipant> EventParticipants { get; set; } = null!;
    
}
