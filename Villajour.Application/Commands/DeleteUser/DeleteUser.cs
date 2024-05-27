using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;

namespace Villajour.Application.Commands.DeleteUser;

public record class DeleteUserCommand : IRequest<bool>
{
    public Guid Id { get; set; }    
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IVilleajourDbContext _context;

    public DeleteUserCommandHandler(IVilleajourDbContext context)
    {
        this._context = context;
    }

    public class DeleteEntity
    {
        public bool ConfirmationDelete { get; set; }
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity != null) 
        {
            this._context.Users.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        else
        {
            return false;
        }
    }
}
