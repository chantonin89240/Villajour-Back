using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Dto;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Documents.GetDocumentHistoByMairie;

public record class GetDocumentHistoByMairieCommand : IRequest<List<DocumentDto>>
{
    public Guid MairieId { get; set; }
}

public class GetDocumentHistoByMairieHandler : IRequestHandler<GetDocumentHistoByMairieCommand, List<DocumentDto>>
{
    private readonly IVilleajourDbContext _context;

    public GetDocumentHistoByMairieHandler(IVilleajourDbContext context)
    {
        _context = context;
    }

    public async Task<List<DocumentDto?>> Handle(GetDocumentHistoByMairieCommand request, CancellationToken cancellationToken)
    {
        List<DocumentDto> entity = await _context.Documents
        .Where(m => m.MairieId == request.MairieId)
        .OrderByDescending(e => e.Date)
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
