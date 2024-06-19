using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Dto;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Events.GetEventByMairieFavorite;

public record class GetEventByMairieFavoriteCommand : IRequest<List<EventByMairieFavoriteDto>>
{
    public Guid UserId { get; set; }
}

public class GetEventByMairieFavoriteHandler : IRequestHandler<GetEventByMairieFavoriteCommand, List<EventByMairieFavoriteDto>>
{
    private readonly IVillajourDbContext _context;

    public GetEventByMairieFavoriteHandler(IVillajourDbContext context)
    {
        _context = context;
    }

    public async Task<List<EventByMairieFavoriteDto?>> Handle(GetEventByMairieFavoriteCommand request, CancellationToken cancellationToken)
    {
        List<EventByMairieFavoriteDto> entity = await (from fm in _context.FavoritesMairie
                                                          join m in _context.Mairies on fm.MairieId equals m.Id
                                                          join d in _context.Events on m.Id equals d.MairieId
                                                          join dt in _context.EventTypes on d.EventTypeId equals dt.Id
                                                          where fm.UserId == request.UserId
                                                          select new EventByMairieFavoriteDto
                                                          {
                                                              Id = d.Id,
                                                              StartTime = d.StartTime,
                                                              EndTime = d.EndTime,
                                                              Address = d.Address,
                                                              Title = d.Title,
                                                              Description = d.Description,
                                                              EventType = new EventTypeEntity
                                                              {
                                                                  Id = dt.Id,
                                                                  Libelle = dt.Libelle
                                                              },
                                                              Mairie = m,
                                                              Favorite = _context.FavoritesContent.Any(fc => fc.UserId == request.UserId && fc.EventId == d.Id)
                                                          })
                                                        .ToListAsync(cancellationToken);

        return entity;
    }
}

