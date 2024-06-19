using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Dto;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Announcements.GetAnnoucementByFavoriteMaire
{
    public class GetAnnoucementByFavoriteMaireCommand : IRequest<List<AnnouncementByMairieFavoriteDto>>
    {
        public Guid UserId { get; set; }

    }

    public class GetAnnouncementByMairieFavoriteHandler : IRequestHandler<GetAnnoucementByFavoriteMaireCommand, List<AnnouncementByMairieFavoriteDto>>
    {
        private readonly IVillajourDbContext _context;

        public GetAnnouncementByMairieFavoriteHandler(IVillajourDbContext context)
        {
            _context = context;
        }

        public async Task<List<AnnouncementByMairieFavoriteDto?>> Handle(GetAnnoucementByFavoriteMaireCommand request, CancellationToken cancellationToken)
        {
            List<AnnouncementByMairieFavoriteDto> entity = await _context.FavoritesMairie
                .Where(f => f.UserId == request.UserId)
                .Join(_context.Mairies, f => f.MairieId, m => m.Id, (f, m) => new { FavoriteMairie = f, Mairie = m })
                .Join(_context.Announcements, fm => fm.Mairie.Id, e => e.MairieId,
                      (fm, e) => new AnnouncementByMairieFavoriteDto
                      {
                          Mairie = fm.Mairie,
                          AnnouncementList = new List<AnnouncementEntity> { e }
                      })
                .GroupBy(e => e.Mairie.Id)
                .Select(g => new AnnouncementByMairieFavoriteDto
                {
                    Mairie = g.First().Mairie,
                    AnnouncementList = g.Select(e => e.AnnouncementList.First()).ToList()
                })
                .ToListAsync(cancellationToken);


            return entity;
        }
    }
}
