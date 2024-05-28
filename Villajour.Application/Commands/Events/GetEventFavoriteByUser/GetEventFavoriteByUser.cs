using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Events.GetEventFavoriteByUser;

public record class GetEventFavoriteByUserCommand : IRequest<List<EventEntity>>
{
    public Guid UserId { get; set; }
}

public class GetEventFavoriteByUserCommandHandler : IRequestHandler<GetEventFavoriteByUserCommand, List<EventEntity>>
{
    private readonly IVilleajourDbContext _context;

    public GetEventFavoriteByUserCommandHandler(IVilleajourDbContext context)
    {
        _context = context;
    }

    public async Task<List<EventEntity?>> Handle(GetEventFavoriteByUserCommand request, CancellationToken cancellationToken)
    {
        List<EventEntity> entity = await _context.Events.OrderByDescending(e => e.EndTime).ToListAsync(cancellationToken);

        return entity;
    }
}
