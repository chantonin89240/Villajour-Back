using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

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

    [Required]
    [Column(Order = 3)]
    public string? Description { get; set; }

    [Required]
    [Column(Order = 4)]
    public byte[]? Document { get; set; }

    [Required]
    [Column(Order = 5)]
    public int DocumentTypeId { get; set; }

    [Required]
    [Column(Order = 6)]
    public int MairieId { get; set; }
}
