using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Dto;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Announcements.GetAnnouncementByMairieDetail;

public class GetAnnouncementByMairieDetailCommand : IRequest<List<AnnouncementByMairieDetailDto>>
{
    public Guid UserId { get; set; }
    public Guid MairieId { get; set; }
}

public class GetAnnouncementByMairieDetailHandler : IRequestHandler<GetAnnouncementByMairieDetailCommand, List<AnnouncementByMairieDetailDto>>
{
    private readonly IVillajourDbContext _context;

    public GetAnnouncementByMairieDetailHandler(IVillajourDbContext context)
    {
        _context = context;
    }

    public async Task<List<AnnouncementByMairieDetailDto?>> Handle(GetAnnouncementByMairieDetailCommand request, CancellationToken cancellationToken)
    {
        List<AnnouncementByMairieDetailDto> entity = await (from d in _context.Announcements
                                                     join dt in _context.AnnouncementTypes on d.AnnouncementTypeId equals dt.Id
                                                     join fc in _context.FavoritesContent on d.Id equals fc.AnnouncementId into fcGroup
                                                     from fc in fcGroup.DefaultIfEmpty()
                                                     where d.MairieId == request.MairieId
                                                     select new AnnouncementByMairieDetailDto
                                                     {
                                                         Id = d.Id,
                                                         Date = d.Date,
                                                         Title = d.Title,
                                                         Description = d.Description,
                                                         AnnouncementType = new AnnouncementTypeEntity
                                                         {
                                                             Id = dt.Id,
                                                             Libelle = dt.Libelle
                                                         },
                                                         Favorite = fc != null && fc.UserId == request.UserId
                                                     })
                                                .Distinct()
                                                .ToListAsync(cancellationToken);

        return entity;
    }
}