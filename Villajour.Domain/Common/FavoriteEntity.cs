using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Villajour.Domain.Common;

public class FavoriteEntity
{
    [Key]
    [Required]
    [Column(Order = 0)]
    public int FavoriteId { get; set; }

    [Required]
    [Column(Order = 1)]
    public int UserId { get; set; }

    [Required]
    [Column(Order = 2)]
    public string? Type { get; set; }
}
