using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Villajour.Domain.Common;

public class FavoriteMairieEntity
{
    [Key]
    [Required]
    [Column(Order = 0)]
    public int Id { get; set; }

    [Required]
    [Column(Order = 1)]
    public Guid UserId { get; set; }

    [Required]
    [Column(Order = 2)]
    public Guid MairieId { get; set; }
}
