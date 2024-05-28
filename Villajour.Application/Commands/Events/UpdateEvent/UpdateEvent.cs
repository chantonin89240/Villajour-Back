using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Events.UpdateEvent;

public record class UpdateEventCommand : IRequest<EventEntity>
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? Address { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int EventTypeId { get; set; }
    public Guid MairieId { get; set; }
}

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, EventEntity>
{
    private readonly IVilleajourDbContext _context;

    public UpdateEventCommandHandler(IVilleajourDbContext context)
    {
        _context = context;
    }

    public async Task<EventEntity?> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Events.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity != null)
        {
            entity.StartTime = request.StartTime;
            entity.EndTime = request.EndTime;
            entity.Address = request.Address;
            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.EventTypeId = request.EventTypeId;
            entity.MairieId = request.MairieId;

            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }
        else
        {
            return null;
        }
    }
}
