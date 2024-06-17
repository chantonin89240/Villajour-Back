using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villajour.Application.Commands.Dto;

public class DeleteFavoriteMairieDto
{
    public Guid UserId { get; set; }
    public Guid MairieId { get; set; }
}
