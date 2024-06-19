using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Dto;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Documents.GetDocumentByMairieDetail;

public class GetDocumentByMairieDetailCommand : IRequest<List<DocumentByMairieDetailDto>>
{
    public Guid UserId { get; set; }
    public Guid MairieId { get; set; }
}


public class GetDocumentByMairieDetailHandler : IRequestHandler<GetDocumentByMairieDetailCommand, List<DocumentByMairieDetailDto>>
{
    private readonly IVillajourDbContext _context;

    public GetDocumentByMairieDetailHandler(IVillajourDbContext context)
    {
        _context = context;
    }

    public async Task<List<DocumentByMairieDetailDto?>> Handle(GetDocumentByMairieDetailCommand request, CancellationToken cancellationToken)
    {
        List<DocumentByMairieDetailDto> entity = await (from d in _context.Documents
                                                        join dt in _context.DocumentTypes on d.DocumentTypeId equals dt.Id
                                                        join fc in _context.FavoritesContent on d.Id equals fc.DocumentId into fcGroup
                                                        from fc in fcGroup.DefaultIfEmpty()
                                                        where d.MairieId == request.MairieId
                                                        select new DocumentByMairieDetailDto
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
                                                            Favorite = fc != null && fc.UserId == request.UserId
                                                        })
                                                .Distinct()
                                                .ToListAsync(cancellationToken);

        return entity;
    }
}
