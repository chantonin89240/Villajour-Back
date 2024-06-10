using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Documents.GetDocumentHistoByMairie;

public record class GetDocumentHistoByMairieCommand : IRequest<List<DocumentEntity>>
{
    public Guid MairieId { get; set; }
}

public class GetDocumentHistoByMairieHandler : IRequestHandler<GetDocumentHistoByMairieCommand, List<DocumentEntity>>
{
    private readonly IVilleajourDbContext _context;

    public GetDocumentHistoByMairieHandler(IVilleajourDbContext context)
    {
        _context = context;
    }

    public async Task<List<DocumentEntity?>> Handle(GetDocumentHistoByMairieCommand request, CancellationToken cancellationToken)
    {
        List<DocumentEntity> entity = await _context.Documents.Where(m => m.MairieId == request.MairieId).OrderByDescending(e => e.Date).ToListAsync(cancellationToken);


        return entity;
    }
}
