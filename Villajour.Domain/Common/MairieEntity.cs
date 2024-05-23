using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    [RegularExpression(@"^\d{15}$")]
    public string? Phone { get; set; }

    [Column(Order = 2)]
    public string? Picture { get; set; }

    [Required]
    [Column(Order = 3)]
    [RegularExpression(@"^\d{14}$")]
    public string? Siret { get; set; }
}
