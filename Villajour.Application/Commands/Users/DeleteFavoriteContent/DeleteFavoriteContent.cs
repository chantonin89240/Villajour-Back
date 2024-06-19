using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Users.DeleteFavoriteContent;

public record class DeleteFavoriteContentCommand : IRequest<bool>
{
    public Guid UserId { get; set; }
    public int? AnnouncementId { get; set; }
    public int? EventId { get; set; }
    public int? DocumentId { get; set; }
}

public class DeleteFavoriteContentCommandHandler : IRequestHandler<DeleteFavoriteContentCommand, bool>
{
    private readonly IVillajourDbContext _context;

    public DeleteFavoriteContentCommandHandler(IVillajourDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteFavoriteContentCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId != Guid.Empty)
        {
            FavoriteContentEntity entity;
            if (request.AnnouncementId.HasValue)
            {
                entity = await _context.FavoritesContent.FirstOrDefaultAsync(x => x.UserId == request.UserId && x.AnnouncementId == request.AnnouncementId, cancellationToken);
            }
            else if (request.EventId.HasValue)
            {
                entity = await _context.FavoritesContent.FirstOrDefaultAsync(x => x.UserId == request.UserId && x.EventId == request.EventId, cancellationToken);
            }
            else 
            {
                entity = await _context.FavoritesContent.FirstOrDefaultAsync(x => x.UserId == request.UserId && x.DocumentId == request.DocumentId, cancellationToken);
            }

            if (entity != null)
            {
                _context.FavoritesContent.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
