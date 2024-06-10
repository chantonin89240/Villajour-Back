using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Documents.GetDocumentFav;

public record class GetDocumentFavCommand : IRequest<List<DocumentEntity>>
{
    public Guid UserId { get; set; }
}

public class GetDocumentFavHandler : IRequestHandler<GetDocumentFavCommand, List<DocumentEntity>>
{
    private readonly IVilleajourDbContext _context;

    public GetDocumentFavHandler(IVilleajourDbContext context)
    {
        _context = context;
    }

    public async Task<List<DocumentEntity?>> Handle(GetDocumentFavCommand request, CancellationToken cancellationToken)
    {
        List<DocumentEntity> entity = await _context.Documents
             .Where(e => _context.FavoritesContent
                 .Where(f => f.UserId == request.UserId && f.DocumentId.HasValue)
                 .Select(f => f.DocumentId.Value)
                 .Contains(e.Id))
             .OrderBy(e => e.Date)
             .ToListAsync(cancellationToken);


        return entity;
    }
}