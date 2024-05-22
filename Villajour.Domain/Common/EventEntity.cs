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
    public DateOnly Date { get; set; }

    [Required]
    [Column(Order = 2)]
    public TimeOnly StartTime { get; set; }

    [Required]
    [Column(Order = 3)]
    public TimeOnly EndTime { get; set; }

    [Required]
    [Column(Order = 4)]
    public string? Address { get; set; }

    [Required]
    [Column(Order = 5)]
    public string? Title { get; set; }

    [Required]
    [Column(Order = 6)]
    public string? Description { get; set; }

    [Required]
    [Column(Order = 7)]
    public int EventTypeId { get; set; }

    [Required]
    [Column(Order = 8)]
    public int MairieId { get; set; }
}
