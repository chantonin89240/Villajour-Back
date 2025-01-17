﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;

namespace Villajour.Application.Commands.Users.DeleteUser;

public record class DeleteUserCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IVillajourDbContext _context;

    public DeleteUserCommandHandler(IVillajourDbContext context)
    {
        _context = context;
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
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        else
        {
            return false;
        }
    }
}
