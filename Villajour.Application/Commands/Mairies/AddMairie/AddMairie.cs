using MediatR;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
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
    public string? Name { get; set; }
    public string? Email { get; set; }

}

public class AddMairieCommandHandler : IRequestHandler<AddMairieCommand, MairieEntity>
{
    private readonly IVillajourDbContext _context;

    public AddMairieCommandHandler(IVillajourDbContext context)
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
            Name = request.Name,
            Email = request.Email,
        };

        _context.Mairies.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;

    }
}
