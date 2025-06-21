using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZadanieDodatkowe.Models;

[Table("Speaker")]
public class Speaker
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(50)]
    public string Surname { get; set; }
    public virtual ICollection<EventSpeaker> EventSpeakers { get; set; } = null!;
}