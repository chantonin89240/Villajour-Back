using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;

namespace Villajour.Application.Commands.Documents.DeleteDocument;

public record class DeleteDocumentCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand, bool>
{
    private readonly IVilleajourDbContext _context;

    public DeleteDocumentCommandHandler(IVilleajourDbContext context)
    {
        _context = context;
    }

    public class DeleteEntity
    {
        public bool ConfirmationDelete { get; set; }
    }

    public async Task<bool> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Documents.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity != null)
        {
            _context.Documents.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        else
        {
            return false;
        }
    }
}
