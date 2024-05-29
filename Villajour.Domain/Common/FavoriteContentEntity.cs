using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Villajour.Domain.Common;

public class FavoriteContentEntity
{
    [Key]
    [Required]
    [Column(Order = 0)]
    public int Id { get; set; }

    [Required]
    [Column(Order = 1)]
    public Guid UserId { get; set; }

    [Column(Order = 2)]
    public int? AnnouncementId { get; set; }

    [Column(Order = 3)]
    public int? EventId { get; set; }

    [Column(Order = 4)]
    public int? DocumentId { get; set; }

}
