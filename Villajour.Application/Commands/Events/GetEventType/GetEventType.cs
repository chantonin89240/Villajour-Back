using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Events.GetEventType;

public record class GetEventTypeCommand : IRequest<List<EventTypeEntity>>
{

}

public class GetEventTypeHandler : IRequestHandler<GetEventTypeCommand, List<EventTypeEntity>>
{
    private readonly IVillajourDbContext _context;

    public GetEventTypeHandler(IVillajourDbContext context)
    {
        _context = context;
    }

    public async Task<List<EventTypeEntity?>> Handle(GetEventTypeCommand request, CancellationToken cancellationToken)
    {
        List<EventTypeEntity> entity = await _context.EventTypes.ToListAsync(cancellationToken);

        return entity;
    }
}
