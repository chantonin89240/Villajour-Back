using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;

namespace Villajour.Application.Commands.Mairies.DeleteMairie;

public record class DeleteMairieCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}

public class DeleteMairieCommandHandler : IRequestHandler<DeleteMairieCommand, bool>
{
    private readonly IVillajourDbContext _context;

    public DeleteMairieCommandHandler(IVillajourDbContext context)
    {
        _context = context;
    }

    public class DeleteEntity
    {
        public bool ConfirmationDelete { get; set; }
    }

    public async Task<bool> Handle(DeleteMairieCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Mairies.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity != null)
        {
            _context.Mairies.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        else
        {
            return false;
        }
    }
}
