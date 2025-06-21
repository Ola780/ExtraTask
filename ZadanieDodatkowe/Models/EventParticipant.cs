using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ZadanieDodatkowe.Models;

[PrimaryKey(nameof(ParticipantId),nameof(EventId))]
[Table("Event_Participant")]
public class EventParticipant
{
    [Column("Participant_Id")]
    public int ParticipantId { get; set; }
    [Column("Event_Id")]
    public int EventId { get; set; }
    
    
    [ForeignKey(nameof(EventId))]
    public virtual Event Event { get; set; } = null!;
    [ForeignKey(nameof(ParticipantId))]
    public virtual Participant Participant{ get; set; } = null!;
}