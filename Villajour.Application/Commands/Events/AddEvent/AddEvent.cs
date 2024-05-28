using MediatR;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Events.AddEvent;

public record class AddEventCommand : IRequest<EventEntity>
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? Address { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int EventTypeId { get; set; }
    public Guid MairieId { get; set; }
}

public class AddEventCommandHandler : IRequestHandler<AddEventCommand, EventEntity>
{
    private readonly IVilleajourDbContext _context;

    public AddEventCommandHandler(IVilleajourDbContext context)
    {
        _context = context;
    }

    public async Task<EventEntity> Handle(AddEventCommand request, CancellationToken cancellationToken)
    {
        var entity = new EventEntity
        {
            EndTime = request.EndTime,
            Address = request.Address,
            Title = request.Title,
            Description = request.Description,
            EventTypeId = request.EventTypeId,
            MairieId = request.MairieId
        };

        _context.Events.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;

    }
}
