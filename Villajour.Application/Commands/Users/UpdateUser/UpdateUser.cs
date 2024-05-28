using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Users.UpdateUser;

public record class UpdateUserCommand : IRequest<UserEntity>
{
    public Guid Id { get; set; }
    public string? Phone { get; set; }
    public string? Picture { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserEntity>
{
    private readonly IVilleajourDbContext _context;

    public UpdateUserCommandHandler(IVilleajourDbContext context)
    {
        _context = context;
    }

    public async Task<UserEntity?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity != null)
        {
            entity.Phone = request.Phone;
            entity.Picture = request.Picture;

            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }
        else
        {
            return null;
        }
    }
}
