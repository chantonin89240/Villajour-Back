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
    private readonly IVillajourDbContext _context;

    public GetEventFavoriteByUserCommandHandler(IVillajourDbContext context)
    {
        _context = context;
    }

    public async Task<List<EventEntity?>> Handle(GetEventFavoriteByUserCommand request, CancellationToken cancellationToken)
    {
        List<EventEntity> entity = await _context.Events
            .Where(e => _context.FavoritesContent
                .Where(f => f.UserId == request.UserId && f.EventId.HasValue)
                .Select(f => f.EventId.Value)
                .Contains(e.Id)
            && e.EndTime >= DateTime.Now)
            .OrderBy(e => e.StartTime)
            .ToListAsync(cancellationToken);

        return entity;
    }
}
