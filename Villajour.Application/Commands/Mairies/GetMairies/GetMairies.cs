using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Mairies.GetMairies;

public record class GetMairiesCommand : IRequest<List<MairieEntity>>;

public class GetMairiesCommandHandler : IRequestHandler<GetMairiesCommand, List<MairieEntity>>
{
    private readonly IVillajourDbContext _context;

    public GetMairiesCommandHandler(IVillajourDbContext context)
    {
        _context = context;
    }

    public async Task<List<MairieEntity>?> Handle(GetMairiesCommand request, CancellationToken cancellationToken)
    {
        List<MairieEntity> entity = await _context.Mairies.ToListAsync(cancellationToken);

        return entity;
    }
}
