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
    private readonly IVilleajourDbContext _context;

    public GetEventByMairieFavoriteHandler(IVilleajourDbContext context)
    {
        _context = context;
    }

    public async Task<List<EventByMairieFavoriteDto?>> Handle(GetEventByMairieFavoriteCommand request, CancellationToken cancellationToken)
    {
        List<EventByMairieFavoriteDto> entity = await _context.FavoritesMairie
            .Where(f => f.UserId == request.UserId)
            .Join(_context.Mairies, f => f.MairieId, m => m.Id, (f, m) => new { FavoriteMairie = f, Mairie = m })
            .Join(_context.Events, fm => fm.Mairie.Id, e => e.MairieId,
                  (fm, e) => new EventByMairieFavoriteDto
                  {
                      Mairie = fm.Mairie,
                      EventList = new List<EventEntity> { e }
                  })
            .GroupBy(e => e.Mairie.Id)
            .Select(g => new EventByMairieFavoriteDto
            {
                Mairie = g.First().Mairie,
                EventList = g.Select(e => e.EventList.First()).ToList()
            })
            .ToListAsync(cancellationToken);


        return entity;
    }
}

