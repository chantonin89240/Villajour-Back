using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;

namespace Villajour.Application.Commands.Users.DeleteFavoriteMairie;

public record class DeleteFavoriteMairieCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class DeleteFavoriteMairieCommandHandler : IRequestHandler<DeleteFavoriteMairieCommand, bool>
{
    private readonly IVilleajourDbContext _context;

    public DeleteFavoriteMairieCommandHandler(IVilleajourDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteFavoriteMairieCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.FavoritesMairie.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity != null)
        {
            _context.FavoritesMairie.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        else
        {
            return false;
        }

    }
}
