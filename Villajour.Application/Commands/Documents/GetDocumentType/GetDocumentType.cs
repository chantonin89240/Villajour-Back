using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Documents.GetDocumentType;

public record class GetDocumentTypeCommand : IRequest<List<DocumentTypeEntity>>
{

}

public class GetDocumentTypeHandler : IRequestHandler<GetDocumentTypeCommand, List<DocumentTypeEntity>>
{
    private readonly IVilleajourDbContext _context;

    public GetDocumentTypeHandler(IVilleajourDbContext context)
    {
        _context = context;
    }

    public async Task<List<DocumentTypeEntity?>> Handle(GetDocumentTypeCommand request, CancellationToken cancellationToken)
    {
        List<DocumentTypeEntity> entity = await _context.DocumentTypes.ToListAsync(cancellationToken);

        return entity;
    }
}