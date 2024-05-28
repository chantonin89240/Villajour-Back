using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Events.GetEventHistoByMairie;

public record class GetEventHistoByMairieCommand : IRequest<List<EventEntity>>
{
    public Guid MairieId { get; set; }
}

public class GetEventHistoByMairieCommandHandler : IRequestHandler<GetEventHistoByMairieCommand, List<EventEntity>>
{
    private readonly IVilleajourDbContext _context;

    public GetEventHistoByMairieCommandHandler(IVilleajourDbContext context)
    {
        _context = context;
    }

    public async Task<List<EventEntity?>> Handle(GetEventHistoByMairieCommand request, CancellationToken cancellationToken)
    {
        List<EventEntity> entity = await _context.Events.Where(m => m.MairieId == request.MairieId && m.EndTime <= DateTime.Now).ToListAsync(cancellationToken);

        return entity;
    }
}
