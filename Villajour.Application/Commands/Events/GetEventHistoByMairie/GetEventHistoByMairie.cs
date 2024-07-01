using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Dto;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Events.GetEventHistoByMairie;

public record class GetEventHistoByMairieCommand : IRequest<List<EventDto>>
{
    public Guid MairieId { get; set; }
}

public class GetEventHistoByMairieHandler : IRequestHandler<GetEventHistoByMairieCommand, List<EventDto>>
{
    private readonly IVillajourDbContext _context;

    public GetEventHistoByMairieHandler(IVillajourDbContext context)
    {
        _context = context;
    }

    public async Task<List<EventDto?>> Handle(GetEventHistoByMairieCommand request, CancellationToken cancellationToken)
    {
        List<EventDto> entity = await _context.Events
        .Where(m => m.MairieId == request.MairieId)
        .OrderByDescending(e => e.EndTime)
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
