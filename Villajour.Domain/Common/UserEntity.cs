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

    [Column(Order = 1)]
    [RegularExpression(@"^\d{10}$")]
    public string? Phone { get; set; }

    [Column(Order = 2)]
    public string? Picture { get; set; }

    [Required]
    [Column(Order = 3)]
    public string? Email { get; set; }
}
