using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Villajour.Domain.Common;

public class UserEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Required]
    [Column(Order = 0)]
    public Guid Id { get; set; }

    [Required]
    [Column(Order = 1)]
    [RegularExpression(@"^\d{15}$")]
    public string? Phone { get; set; }

    [Column(Order = 2)]
    public string? Picture { get; set; }
}
