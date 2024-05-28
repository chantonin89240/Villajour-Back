using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villajour.Domain.Common;

public class FavoriteMairieEntity
{
    [Key]
    [Required]
    [Column(Order = 0)]
    public Guid Id { get; set; }

    [Required]
    [Column(Order = 1)]
    public Guid UserId { get; set; }

    [Required]
    [Column(Order = 2)]
    public Guid MairieId { get; set; }

    
}
