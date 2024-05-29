using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Mairies.GetMairieById;

public record class GetMairieByIdCommand : IRequest<MairieEntity>
{
    public Guid Id { get; set; }
}

public class GetMairieByIdCommandHandler : IRequestHandler<GetMairieByIdCommand, MairieEntity>
{
    private readonly IVilleajourDbContext _context;

    public GetMairieByIdCommandHandler(IVilleajourDbContext context)
    {
        _context = context;
    }

    public async Task<MairieEntity?> Handle(GetMairieByIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Mairies.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

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
