using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;

namespace Villajour.Application.Commands.Users.DeleteFavoriteContent;

public record class DeleteFavoriteContentCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class DeleteFavoriteContentCommandHandler : IRequestHandler<DeleteFavoriteContentCommand, bool>
{
    private readonly IVilleajourDbContext _context;

    public DeleteFavoriteContentCommandHandler(IVilleajourDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteFavoriteContentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.FavoritesContent.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

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
}
