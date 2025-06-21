using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ZadanieDodatkowe.Models;

[PrimaryKey(nameof(SpeakerId),nameof(EventId))]
[Table("Event_Speaker")]
public class EventSpeaker
{
    [Column("Speaker_Id")]
    public int SpeakerId { get; set; }
    [Column("Event_Id")]
    public int EventId { get; set; }
    
    
    [ForeignKey(nameof(EventId))]
    public virtual Event Event { get; set; } = null!;
    [ForeignKey(nameof(SpeakerId))]
    public virtual Speaker Speaker { get; set; } = null!;
}