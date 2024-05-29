using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Events.GetEventComingByMairie;

public record class GetEventComingByMairieCommand : IRequest<List<EventEntity>>
{
    public Guid MairieId { get; set; }
}

public class GetEventComingByMairieCommandHandler : IRequestHandler<GetEventComingByMairieCommand, List<EventEntity>>
{
    private readonly IVilleajourDbContext _context;

    public GetEventComingByMairieCommandHandler(IVilleajourDbContext context)
    {
        _context = context;
    }

    public async Task<List<EventEntity?>> Handle(GetEventComingByMairieCommand request, CancellationToken cancellationToken)
    {
        List<EventEntity> entity = await _context.Events.Where(m => m.MairieId == request.MairieId && m.StartTime >= DateTime.Now).ToListAsync(cancellationToken);

        return entity;
    }
}
