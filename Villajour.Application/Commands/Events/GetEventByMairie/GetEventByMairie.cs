﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Events.GetEventByMairie;

public record class GetEventByMairieCommand : IRequest<List<EventEntity>>
{
    public Guid MairieId { get; set; }
}

public class GetEventComingByMairieCommandHandler : IRequestHandler<GetEventByMairieCommand, List<EventEntity>>
{
    private readonly IVilleajourDbContext _context;

    public GetEventComingByMairieCommandHandler(IVilleajourDbContext context)
    {
        _context = context;
    }

    public async Task<List<EventEntity?>> Handle(GetEventByMairieCommand request, CancellationToken cancellationToken)
    {
        List<EventEntity> entity = await _context.Events.Where(m => m.MairieId == request.MairieId).OrderByDescending(e => e.EndTime).ToListAsync(cancellationToken);

        return entity;
    }
}