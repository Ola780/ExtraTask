using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZadanieDodatkowe.Models;

[Table("Participant")]
public class Participant
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(50)]
    public string Surname { get; set; }
    public virtual ICollection<EventParticipant> EventParticipants { get; set; } = null!;
}