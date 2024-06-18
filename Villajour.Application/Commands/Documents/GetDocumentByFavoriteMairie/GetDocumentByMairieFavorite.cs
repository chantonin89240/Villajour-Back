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
        List<DocumentByMairieFavoriteDto> entity = await (from fm in _context.FavoritesMairie
                                                          join m in _context.Mairies on fm.MairieId equals m.Id
                                                          join d in _context.Documents on m.Id equals d.MairieId
                                                          join dt in _context.DocumentTypes on d.DocumentTypeId equals dt.Id
                                                          where fm.UserId == request.UserId
                                                          select new DocumentByMairieFavoriteDto
                                                          {
                                                              Id = d.Id,
                                                              Date = d.Date,
                                                              Title = d.Title,
                                                              Description = d.Description,
                                                              DocumentUrl = d.DocumentUrl,
                                                              DocumentType = new DocumentTypeEntity
                                                              {
                                                                  Id = dt.Id,
                                                                  Libelle = dt.Libelle
                                                              },
                                                              Mairie = m,
                                                              Favorite = _context.FavoritesContent.Any(fc => fc.UserId == request.UserId && fc.DocumentId == d.Id)
                                                          })
                                                        .ToListAsync(cancellationToken);

        return entity;


    }
}

