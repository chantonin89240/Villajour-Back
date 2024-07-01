using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Dto;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Announcements.GetAnnouncementHistoByMairie;

public record class GetAnnouncementHistoByMairieCommand : IRequest<List<AnnouncementDto>>
{
    public Guid MairieId { get; set; }
}

public class GetAnnouncementHistoByMairieHandler : IRequestHandler<GetAnnouncementHistoByMairieCommand, List<AnnouncementDto>>
{
    private readonly IVillajourDbContext _context;

    public GetAnnouncementHistoByMairieHandler(IVillajourDbContext context)
    {
        _context = context;
    }

    public async Task<List<AnnouncementDto?>> Handle(GetAnnouncementHistoByMairieCommand request, CancellationToken cancellationToken)
    {
        List<AnnouncementDto> entity = await _context.Announcements
        .Where(m => m.MairieId == request.MairieId)
        .OrderByDescending(e => e.Date)
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
                Address = m.Address
            }
        })
        .ToListAsync(cancellationToken);

        return entity;
    }
}