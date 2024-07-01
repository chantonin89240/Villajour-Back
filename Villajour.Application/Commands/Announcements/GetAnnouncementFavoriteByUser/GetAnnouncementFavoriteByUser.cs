using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Dto;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Announcements.GetAnnouncementFavoriteByUser;

public record class GetAnnouncementFavoriteByUserCommand : IRequest<List<AnnouncementDto>>
{
    public Guid UserId { get; set; }
}

public class GetAnnouncementFavoriteByUserCommandHandler : IRequestHandler<GetAnnouncementFavoriteByUserCommand, List<AnnouncementDto>>
{
    private readonly IVillajourDbContext _context;

    public GetAnnouncementFavoriteByUserCommandHandler(IVillajourDbContext context)
    {
        _context = context;
    }

    public async Task<List<AnnouncementDto?>> Handle(GetAnnouncementFavoriteByUserCommand request, CancellationToken cancellationToken)
    {
        List<AnnouncementDto> entity = await _context.Announcements
            .Where(e => _context.FavoritesContent
                .Where(f => f.UserId == request.UserId && f.AnnouncementId.HasValue)
                .Select(f => f.AnnouncementId.Value)
                .Contains(e.Id))
            .OrderBy(f => f.Date)
            .Join(_context.AnnouncementTypes, d => d.AnnouncementTypeId, dt => dt.Id, (d, dt) => new { Announcement = d, AnnouncementType = dt })
            .Join(_context.Mairies, ddt => ddt.Announcement.MairieId, m => m.Id, (ddt, m) => new AnnouncementDto
            {
                Id = ddt.Announcement.Id,
                Date = ddt.Announcement.Date,
                Title = ddt.Announcement.Title,
                Description = ddt.Announcement.Description,
                AnnouncementType = new AnnouncementTypeEntity
                {
                    Id = ddt.AnnouncementType.Id,
                    Libelle = ddt.AnnouncementType.Libelle
                },
                Mairie = new MairieEntity
                {
                    Id = m.Id,
                    Phone = m.Phone,
                    Picture = m.Picture,
                    Siret = m.Siret,
                    Address = m.Address,
                    Name = m.Name,
                    Email = m.Email,
                }
            })
             .ToListAsync(cancellationToken);


        return entity;
    }
}