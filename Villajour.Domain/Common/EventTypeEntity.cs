using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Villajour.Domain.Common;

public class EventTypeEntity
{
    [Key]
    [Required]
    [Column(Order = 0)]
    public int EventTypeId { get; set; }

    [Required]
    [Column(Order = 1)]
    public string? Libelle { get; set; }
}
