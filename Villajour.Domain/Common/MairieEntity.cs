using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Villajour.Domain.Common;

public class MairieEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Required]
    [Column(Order = 0)]
    public Guid Id { get; set; }

    [Required]
    [Column(Order = 1)]
    [RegularExpression(@"^\d{10}$")]
    public string? Phone { get; set; }

    [Column(Order = 2)]
    public string? Picture { get; set; }

    [Required]
    [Column(Order = 3)]
    [RegularExpression(@"^\d{14}$")]
    public string? Siret { get; set; }

    [Required]
    [Column(Order = 4)]
    public string? Address { get; set; }

    [Required]
    [Column(Order = 5)]
    public string? Name { get; set; }

    [Required]
    [Column(Order = 6)]
    public string? Email { get; set; }
}
