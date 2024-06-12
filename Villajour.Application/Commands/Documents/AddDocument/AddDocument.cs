using MediatR;
using Microsoft.AspNetCore.Http;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Documents.AddDocument;

public record class AddDocumentCommand : IRequest<DocumentEntity>
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? DocumentUrl { get; set; }
    public int DocumentTypeId { get; set; }
    public Guid MairieId { get; set; }
}

public class AddDocumentCommandHandler : IRequestHandler<AddDocumentCommand, DocumentEntity>
{
    private readonly IVilleajourDbContext _context;

    public AddDocumentCommandHandler(IVilleajourDbContext context)
    {
        _context = context;
    }

    public async Task<DocumentEntity> Handle(AddDocumentCommand request, CancellationToken cancellationToken)
    {
        var entity = new DocumentEntity
        {
            Title = request.Title,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Description = request.Description,
            DocumentUrl = request.DocumentUrl,
            DocumentTypeId = request.DocumentTypeId,
            MairieId = request.MairieId
        };

        _context.Documents.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;

    }
}
