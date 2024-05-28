using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Villajour.Domain.Common;

public class EventEntity
{
    [Key]
    [Required]
    [Column(Order = 0)]
    public int Id { get; set; }

    [Required]
    [Column(Order = 1)]
    public DateTime StartTime { get; set; }

    [Required]
    [Column(Order = 2)]
    public DateTime EndTime { get; set; }

    [Column(Order = 3)]
    public string? Address { get; set; }

    [Required]
    [Column(Order = 4)]
    public string? Title { get; set; }

    [Column(Order = 5)]
    public string? Description { get; set; }

    [Required]
    [Column(Order = 6)]
    public int EventTypeId { get; set; }

    [Required]
    [Column(Order = 7)]
    public Guid MairieId { get; set; }
}
