using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Villajour.Domain.Common;

public class ContactEntity
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
    public int ContactTypeId { get; set; }

    [Required]
    [Column(Order = 5)]
    public Guid MairieId { get; set; }

    [Required]
    [Column(Order = 6)]
    public Guid UserId { get; set; }
}
