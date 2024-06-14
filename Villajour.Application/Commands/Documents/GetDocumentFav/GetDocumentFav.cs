using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Dto;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Documents.GetDocumentFav;

public record class GetDocumentFavCommand : IRequest<List<DocumentDto>>
{
    public Guid UserId { get; set; }
}

public class GetDocumentFavHandler : IRequestHandler<GetDocumentFavCommand, List<DocumentDto>>
{
    private readonly IVilleajourDbContext _context;

    public GetDocumentFavHandler(IVilleajourDbContext context)
    {
        _context = context;
    }

    public async Task<List<DocumentDto?>> Handle(GetDocumentFavCommand request, CancellationToken cancellationToken)
    {
        List<DocumentDto> entity = await _context.Documents
            .Where(e => _context.FavoritesContent
                .Where(f => f.UserId == request.UserId && f.DocumentId.HasValue)
                .Select(f => f.DocumentId.Value)
                .Contains(e.Id))
            .OrderBy(e => e.Date)
            .Join(_context.DocumentTypes, d => d.DocumentTypeId, dt => dt.Id, (d, dt) => new { Document = d, DocumentType = dt })
            .Join(_context.Mairies, ddt => ddt.Document.MairieId, m => m.Id, (ddt, m) => new DocumentDto
            {
                Id = ddt.Document.Id,
                Date = ddt.Document.Date,
                Title = ddt.Document.Title,
                Description = ddt.Document.Description,
                DocumentUrl = ddt.Document.DocumentUrl,
                DocumentType = new DocumentTypeEntity
                {
                    Id = ddt.DocumentType.Id,
                    Libelle = ddt.DocumentType.Libelle
                },
                Mairie = new MairieEntity
                {
                    Id = m.Id,
                    Phone = m.Phone,
                    Picture = m.Picture,
                    Siret = m.Siret,
                    Address = m.Address
                }
            })
             .ToListAsync(cancellationToken);


        return entity;
    }
}