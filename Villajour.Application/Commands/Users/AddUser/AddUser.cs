﻿using MediatR;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Users.AddUser;

public record class AddUserCommand : IRequest<UserEntity>
{
    public Guid Id { get; set; }
    public string? Phone { get; set; }
    public string? Picture { get; set; }
    public string? Email { get; set; }
}

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserEntity>
{
    private readonly IVillajourDbContext _context;

    public AddUserCommandHandler(IVillajourDbContext context)
    {
        _context = context;
    }

    public async Task<UserEntity> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var entity = new UserEntity
        {
            Id = request.Id,
            Phone = request.Phone,
            Picture = request.Picture,
            Email = request.Email
        };

        _context.Users.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;

    }

}
