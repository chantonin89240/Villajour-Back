using MediatR;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Users.AddFavoriteMairie;


public record class AddFavoriteMairieCommand : IRequest<FavoriteMairieEntity>
{
    public Guid UserId { get; set; }
    public Guid MairieId { get; set; }
}

public class AddFavoriteMairieCommandHandler : IRequestHandler<AddFavoriteMairieCommand, FavoriteMairieEntity>
{
    private readonly IVilleajourDbContext _context;

    public AddFavoriteMairieCommandHandler(IVilleajourDbContext context)
    {
        _context = context;
    }

    public async Task<FavoriteMairieEntity> Handle(AddFavoriteMairieCommand request, CancellationToken cancellationToken)
    {
        var entity = new FavoriteMairieEntity
        {
            UserId = request.UserId,
            MairieId = request.MairieId
        };

        _context.FavoritesMairie.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;

    }

}