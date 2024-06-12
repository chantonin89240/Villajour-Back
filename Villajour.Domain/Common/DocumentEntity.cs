using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Villajour.Domain.Common;

public class DocumentEntity
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

    [Column(Order = 3)]
    public string? Description { get; set; }

    [Required]
    [Column(Order = 4)]
    public string? DocumentUrl { get; set; }

    [Required]
    [Column(Order = 5)]
    public int DocumentTypeId { get; set; }

    [Required]
    [Column(Order = 6)]
    public Guid MairieId { get; set; }
}
