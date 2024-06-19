using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Users.GetUserById;

public record class GetUserByIdCommand : IRequest<UserEntity>
{
    public Guid Id { get; set; }
}

public class GetUserByIdCommandHandler : IRequestHandler<GetUserByIdCommand, UserEntity>
{
    private readonly IVillajourDbContext _context;

    public GetUserByIdCommandHandler(IVillajourDbContext context)
    {
        _context = context;
    }

    public async Task<UserEntity?> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity != null)
        {
            return entity;
        }
        else
        {
            return null;
        }
    }
}
