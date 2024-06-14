using MediatR;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Users.AddFavoriteContent;

public record class AddFavoriteContentCommand : IRequest<FavoriteContentEntity>
{
    public Guid UserId { get; set; }
    public int? AnnouncementId { get; set; }
    public int? EventId { get; set; }
    public int? DocumentId { get; set; }
}

public class AddFavoriteContentCommandHandler : IRequestHandler<AddFavoriteContentCommand, FavoriteContentEntity>
{
    private readonly IVilleajourDbContext _context;

    public AddFavoriteContentCommandHandler(IVilleajourDbContext context)
    {
        _context = context;
    }

    public async Task<FavoriteContentEntity> Handle(AddFavoriteContentCommand request, CancellationToken cancellationToken)
    {
        var entity = new FavoriteContentEntity
        {
            UserId = request.UserId,
            AnnouncementId = request.AnnouncementId,
            EventId = request.EventId,
            DocumentId = request.DocumentId,
        };

        _context.FavoritesContent.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;

    }
}