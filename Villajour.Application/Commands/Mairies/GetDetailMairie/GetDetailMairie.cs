using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Dto;
using Villajour.Application.Commands.Interface;

namespace Villajour.Application.Commands.Mairies.GetDetailMairie;

public record class GetDetailMairieCommand : IRequest<DetailMairieDto>
{
    public Guid UserId { get; set; }
    public Guid MairieId { get; set; }
}

public class GetDetailMairieCommandHandler : IRequestHandler<GetDetailMairieCommand, DetailMairieDto>
{
    private readonly IVillajourDbContext _context;

    public GetDetailMairieCommandHandler(IVillajourDbContext context)
    {
        _context = context;
    }

    public async Task<DetailMairieDto?> Handle(GetDetailMairieCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Mairies
                .Where(x => x.Id == request.MairieId)
                .Select(x => new DetailMairieDto
                {
                    Id = x.Id,
                    Phone = x.Phone,
                    Picture = x.Picture,
                    Siret = x.Siret,
                    Address = x.Address,
                    Name = x.Name,
                    Email = x.Email,
                    Favorite = _context.FavoritesMairie.Any(f => f.MairieId == request.MairieId && f.UserId == request.UserId)
                })
                .FirstOrDefaultAsync(cancellationToken);

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
