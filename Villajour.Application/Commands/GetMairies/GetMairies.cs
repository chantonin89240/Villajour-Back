using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.GetMairies;

public record class GetMairiesCommand : IRequest<List<MairieEntity>>;

public class GetMairiesCommandHandler : IRequestHandler<GetMairiesCommand, List<MairieEntity>>
{
    private readonly IVilleajourDbContext _context;

    public GetMairiesCommandHandler(IVilleajourDbContext context)
    {
        this._context = context;
    }

    public async Task<List<MairieEntity>?> Handle(GetMairiesCommand request, CancellationToken cancellationToken)
    {
        List<MairieEntity> entity = await _context.Mairies.ToListAsync(cancellationToken);

        return entity;
    }
}
