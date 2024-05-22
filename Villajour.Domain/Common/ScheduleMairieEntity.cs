using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Villajour.Domain.Common;

public class ScheduleMairieEntity
{
    [Key]
    [Required]
    [Column(Order = 0)]
    public int ScheduleId { get; set; }

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
    public int MairieId { get; set; }
}
