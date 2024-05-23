using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Villajour.Domain.Common;

public class AppointmentEntity
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

    [Required]
    [Column(Order = 3)]
    public string? Title { get; set; }

    [Required]
    [Column(Order = 4)]
    public string? Description { get; set; }

    [Required]
    [Column(Order = 5)]
    public string? Validation { get; set; }

    [Required]
    [Column(Order = 6)]
    public int AppointmentTypeId { get; set; }

    [Required]
    [Column(Order = 7)]
    public int MairieId { get; set; }

    [Required]
    [Column(Order = 8)]
    public int UserId { get; set; }
}
