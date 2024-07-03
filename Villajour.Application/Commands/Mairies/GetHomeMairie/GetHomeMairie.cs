using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Dto;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Mairies.GetHomeMairie;

public record class GetHomeMairieCommand : IRequest<HomeMairieDto>
{
    public Guid MairieId { get; set; }
}

public class GetHomeMairieCommandHandler : IRequestHandler<GetHomeMairieCommand, HomeMairieDto>
{
    private readonly IVillajourDbContext _context;

    public GetHomeMairieCommandHandler(IVillajourDbContext context)
    {
        _context = context;
    }

    public async Task<HomeMairieDto?> Handle(GetHomeMairieCommand request, CancellationToken cancellationToken)
    {
        HomeMairieDto? entity = await (from m in _context.Mairies
                                    where m.Id == request.MairieId
                                    select new HomeMairieDto
                                    {
                                        Announcement = (from a in _context.Announcements
                                                        join at in _context.AnnouncementTypes on a.AnnouncementTypeId equals at.Id
                                                        where a.MairieId == m.Id
                                                        orderby a.Id descending
                                                        select new AnnouncementDto
                                                        {
                                                            Id = a.Id,
                                                            Date = a.Date,
                                                            Title = a.Title,
                                                            Description = a.Description,
                                                            AnnouncementType = at,
                                                            Mairie = null
                                                        }).FirstOrDefault(),

                                        Event = (from e in _context.Events
                                                 join et in _context.EventTypes on e.EventTypeId equals et.Id
                                                 where e.MairieId == m.Id
                                                 orderby e.Id descending
                                                 select new EventDto
                                                 {
                                                     Id = e.Id,
                                                     StartTime = e.StartTime,
                                                     EndTime = e.EndTime,
                                                     Address = e.Address,
                                                     Title = e.Title,
                                                     Description = e.Description,
                                                     EventType = et,
                                                     Mairie = null
                                                 }).FirstOrDefault(),

                                        Document = (from d in _context.Documents
                                                 join dt in _context.DocumentTypes on d.DocumentTypeId equals dt.Id
                                                 where d.MairieId == m.Id
                                                 orderby d.Id descending
                                                 select new DocumentDto
                                                 {
                                                     Id = d.Id,
                                                     Date = d.Date,
                                                     Title = d.Title,
                                                     Description = d.Description,
                                                     DocumentUrl = d.DocumentUrl,
                                                     DocumentType = dt,
                                                     Mairie = null
                                                 }).FirstOrDefault(),

                                    })
                                    .Distinct()
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