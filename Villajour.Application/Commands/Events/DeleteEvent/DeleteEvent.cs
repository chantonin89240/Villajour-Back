using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;

namespace Villajour.Application.Commands.Events.DeleteEvent;

public record class DeleteEventCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class DeleteMairieCommandHandler : IRequestHandler<DeleteEventCommand, bool>
{
    private readonly IVilleajourDbContext _context;

    public DeleteMairieCommandHandler(IVilleajourDbContext context)
    {
        _context = context;
    }

    public class DeleteEntity
    {
        public bool ConfirmationDelete { get; set; }
    }

    public async Task<bool> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Events.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity != null)
        {
            _context.Events.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        else
        {
            return false;
        }
    }
}
