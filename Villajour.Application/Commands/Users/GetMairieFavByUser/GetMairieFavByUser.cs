using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Dto;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Users.GetMairieFavByUser;

public record class GetMairieFavByUserCommand : IRequest<List<UserMairieFav>>
{
    public Guid UserId { get; set; }
}

public class GetMairieFavByUserCommandHandler : IRequestHandler<GetMairieFavByUserCommand, List<UserMairieFav>>
{
    private readonly IVillajourDbContext _context;

    public GetMairieFavByUserCommandHandler(IVillajourDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserMairieFav?>> Handle(GetMairieFavByUserCommand request, CancellationToken cancellationToken)
    {
        List<UserMairieFav> entity = await (from fm in _context.FavoritesMairie
                                            join m in _context.Mairies on fm.MairieId equals m.Id
                                            where fm.UserId == request.UserId
                                            select new UserMairieFav
                                            {
                                                listMairies = new MairieEntity
                                                {
                                                    Id = m.Id,
                                                    Phone = m.Phone,
                                                    Picture = m.Picture,
                                                    Siret = m.Siret,
                                                    Address = m.Address,
                                                    Name = m.Name,
                                                    Email = m.Email
                                                }
                                            })
                        .ToListAsync(cancellationToken);

        return entity;
    }
}
