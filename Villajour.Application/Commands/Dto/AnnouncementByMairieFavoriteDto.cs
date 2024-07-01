using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Dto
{
    public class AnnouncementByMairieFavoriteDto
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public AnnouncementTypeEntity? AnnouncementType { get; set; }
        public MairieEntity? Mairie { get; set; }
        public bool Favorite { get; set; }
       
    }
}
