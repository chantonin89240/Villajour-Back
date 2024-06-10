using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Dto;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Documents.GetDocumentByFavoriteMairie;

public record class GetDocumentByMairieFavoriteCommand : IRequest<List<DocumentByMairieFavoriteDto>>
{
public Guid UserId { get; set; }
}

public class GetDocumentByMairieFavoriteHandler : IRequestHandler<GetDocumentByMairieFavoriteCommand, List<DocumentByMairieFavoriteDto>>
{
    private readonly IVilleajourDbContext _context;

    public GetDocumentByMairieFavoriteHandler(IVilleajourDbContext context)
    {
        _context = context;
    }

    public async Task<List<DocumentByMairieFavoriteDto?>> Handle(GetDocumentByMairieFavoriteCommand request, CancellationToken cancellationToken)
    {
        List<DocumentByMairieFavoriteDto> entity = await _context.FavoritesMairie
            .Where(f => f.UserId == request.UserId)
            .Join(_context.Mairies, f => f.MairieId, m => m.Id, (f, m) => new { FavoriteMairie = f, Mairie = m })
            .Join(_context.Documents, fm => fm.Mairie.Id, e => e.MairieId,
                  (fm, e) => new DocumentByMairieFavoriteDto
                  {
                      Mairie = fm.Mairie,
                      DocumentList = new List<DocumentEntity> { e }
                  })
            .GroupBy(e => e.Mairie.Id)
            .Select(g => new DocumentByMairieFavoriteDto
            {
                Mairie = g.First().Mairie,
                DocumentList = g.Select(e => e.DocumentList.First()).ToList()
            })
            .ToListAsync(cancellationToken);


        return entity;
    }
}

