using MediatR;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Documents.AddDocument;

public record class AddDocumentCommand : IRequest<DocumentEntity>
{
    public string? Title { get; set; }
    public DateOnly Date { get; set; }
    public string? Description { get; set; }
    public byte[]? Document { get; set; }
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
            Document = request.Document,
            DocumentTypeId = request.DocumentTypeId,
            MairieId = request.MairieId
        };

        _context.Documents.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;

    }
}
