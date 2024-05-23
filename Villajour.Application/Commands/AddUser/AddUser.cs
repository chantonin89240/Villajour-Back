using MediatR;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.AddUser;

public record class AddUserCommand : IRequest<UserEntity>
{
    public string? Picture { get; set; }
}

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserEntity>
{
    private readonly IVilleajourDbContext _context;

    public AddUserCommandHandler(IVilleajourDbContext context)
    {
        this._context = context;
    }

    public async Task<UserEntity> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var entity = new UserEntity
        {
            Picture = request.Picture,
        };

        _context.Users.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;

    }

}
