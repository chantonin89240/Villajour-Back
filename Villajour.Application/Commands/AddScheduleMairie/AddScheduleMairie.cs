using MediatR;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.AddScheduleMairie;

public record class AddScheduleMairieCommand : IRequest<ScheduleMairieEntity>
{
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public Guid MairieId { get; set; }
}

public class AddScheduleMairieCommandHandler : IRequestHandler<AddScheduleMairieCommand, ScheduleMairieEntity>
{
    private readonly IVilleajourDbContext _context;

    public AddScheduleMairieCommandHandler(IVilleajourDbContext context)
    {
        this._context = context;
    }

    public async Task<ScheduleMairieEntity> Handle(AddScheduleMairieCommand request, CancellationToken cancellationToken)
    {
        var entity = new ScheduleMairieEntity
        {
            Date = request.Date,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            MairieId = request.MairieId
        };

        _context.ScheduleMairies.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;

    }

}
