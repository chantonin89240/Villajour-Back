using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Villajour.Domain.Common;

public class AnnouncementEntity
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
    public string? Title { get; set; }

    [Required]
    [Column(Order = 3)]
    public string?  Description { get; set; }

    [Required]
    [Column(Order = 4)]
    public int AnnouncementTypeId { get; set; }

    [Required]
    [Column(Order = 5)]
    public int MairieId { get; set; }
}
