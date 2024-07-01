using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Dto;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Announcements.GetAnnouncementByMairieFavorite;

public record class GetAnnouncementByMairieFavoriteCommand : IRequest<List<AnnouncementByMairieFavoriteDto>>
{
    public Guid UserId { get; set; }
}

public class GetAnnouncementByMairieFavoriteHandler : IRequestHandler<GetAnnouncementByMairieFavoriteCommand, List<AnnouncementByMairieFavoriteDto>>
{
    private readonly IVillajourDbContext _context;

    public GetAnnouncementByMairieFavoriteHandler(IVillajourDbContext context)
    {
        _context = context;
    }

    public async Task<List<AnnouncementByMairieFavoriteDto?>> Handle(GetAnnouncementByMairieFavoriteCommand request, CancellationToken cancellationToken)
    {
        List<AnnouncementByMairieFavoriteDto> entity = await (from fm in _context.FavoritesMairie
                                                       join m in _context.Mairies on fm.MairieId equals m.Id
                                                       join d in _context.Announcements on m.Id equals d.MairieId
                                                       join dt in _context.AnnouncementTypes on d.AnnouncementTypeId equals dt.Id
                                                       where fm.UserId == request.UserId
                                                       select new AnnouncementByMairieFavoriteDto
                                                       {
                                                           Id = d.Id,
                                                           Date = d.Date,
                                                           Title = d.Title,
                                                           Description = d.Description,
                                                           AnnouncementType = new AnnouncementTypeEntity
                                                           {
                                                               Id = dt.Id,
                                                               Libelle = dt.Libelle
                                                           },
                                                           Mairie = m,
                                                           Favorite = _context.FavoritesContent.Any(fc => fc.UserId == request.UserId && fc.AnnouncementId == d.Id)
                                                       })
                                                        .ToListAsync(cancellationToken);

        return entity;
    }
}