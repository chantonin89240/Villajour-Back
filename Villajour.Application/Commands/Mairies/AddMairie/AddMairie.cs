using MediatR;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Mairies.AddMairie;

public record class AddMairieCommand : IRequest<MairieEntity>
{
    public Guid Id { get; set; }
    public string? Phone { get; set; }
    public string? Picture { get; set; }
    public string? Siret { get; set; }
    public string? Address { get; set; }

}

public class AddMairieCommandHandler : IRequestHandler<AddMairieCommand, MairieEntity>
{
    private readonly IVilleajourDbContext _context;

    public AddMairieCommandHandler(IVilleajourDbContext context)
    {
        _context = context;
    }

    public async Task<MairieEntity> Handle(AddMairieCommand request, CancellationToken cancellationToken)
    {
        var entity = new MairieEntity
        {
            Id = request.Id,
            Phone = request.Phone,
            Picture = request.Picture,
            Siret = request.Siret,
            Address = request.Address,
        };

        _context.Mairies.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;

    }
}
