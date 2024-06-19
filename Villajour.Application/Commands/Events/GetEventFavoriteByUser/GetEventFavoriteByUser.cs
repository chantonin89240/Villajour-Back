using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Dto;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Events.GetEventFavoriteByUser;

public record class GetEventFavoriteByUserCommand : IRequest<List<EventDto>>
{
    public Guid UserId { get; set; }
}

public class GetEventFavoriteByUserCommandHandler : IRequestHandler<GetEventFavoriteByUserCommand, List<EventDto>>
{
    private readonly IVillajourDbContext _context;

    public GetEventFavoriteByUserCommandHandler(IVillajourDbContext context)
    {
        _context = context;
    }

    public async Task<List<EventDto?>> Handle(GetEventFavoriteByUserCommand request, CancellationToken cancellationToken)
    {
        List<EventDto> entity = await _context.Events
            .Where(e => _context.FavoritesContent
                .Where(f => f.UserId == request.UserId && f.EventId.HasValue)
                .Select(f => f.EventId.Value)
                .Contains(e.Id))
            .OrderBy(f => f.StartTime)
            .Join(_context.EventTypes, d => d.EventTypeId, dt => dt.Id, (d, dt) => new { Event = d, EventType = dt })
            .Join(_context.Mairies, ddt => ddt.Event.MairieId, m => m.Id, (ddt, m) => new EventDto
            {
                Id = ddt.Event.Id,
                StartTime = ddt.Event.StartTime,
                EndTime = ddt.Event.EndTime,
                Address = ddt.Event.Address,
                Title = ddt.Event.Title,
                Description = ddt.Event.Description,
                EventType = new EventTypeEntity
                {
                    Id = ddt.EventType.Id,
                    Libelle = ddt.EventType.Libelle
                },
                Mairie = new MairieEntity
                {
                    Id = m.Id,
                    Phone = m.Phone,
                    Picture = m.Picture,
                    Siret = m.Siret,
                    Address = m.Address
                }
            })
             .ToListAsync(cancellationToken);


        return entity;
    }
}
