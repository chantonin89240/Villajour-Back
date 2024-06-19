using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Dto;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Events.GetEventByMairieDetail;

public class GetEventByMairieDetailCommand : IRequest<List<EventByMairieDetailDto>>
{
    public Guid UserId { get; set; }
    public Guid MairieId { get; set; }
}


public class GetEventByMairieDetailHandler : IRequestHandler<GetEventByMairieDetailCommand, List<EventByMairieDetailDto>>
{
    private readonly IVilleajourDbContext _context;

    public GetEventByMairieDetailHandler(IVilleajourDbContext context)
    {
        _context = context;
    }

    public async Task<List<EventByMairieDetailDto?>> Handle(GetEventByMairieDetailCommand request, CancellationToken cancellationToken)
    {
        List<EventByMairieDetailDto> entity = await (from d in _context.Events
                                                        join dt in _context.EventTypes on d.EventTypeId equals dt.Id
                                                        join fc in _context.FavoritesContent on d.Id equals fc.EventId into fcGroup
                                                        from fc in fcGroup.DefaultIfEmpty()
                                                        where d.MairieId == request.MairieId
                                                        select new EventByMairieDetailDto
                                                        {
                                                            Id = d.Id,
                                                            StartTime = d.StartTime,
                                                            EndTime = d.EndTime,
                                                            Address = d.Address,
                                                            Title = d.Title,
                                                            Description = d.Description,
                                                            EventType = new EventTypeEntity
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
