using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Villajour.Domain.Common;

public class DocumentTypeEntity
{
    [Key]
    [Required]
    [Column(Order = 0)]
    public int DocumentTypeId { get; set; }

    [Required]
    [Column(Order = 1)]
    public string? Libelle { get; set; }
}
