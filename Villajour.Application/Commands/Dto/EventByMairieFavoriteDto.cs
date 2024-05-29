using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Dto;

public class EventByMairieFavoriteDto
{
    public MairieEntity? Mairie { get; set; }
    public List<EventEntity>? EventList { get; set; }
}
