using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.UpdateMairie;

public record class UpdateMairieCommand : IRequest<MairieEntity>
{
    public Guid Id { get; set; }
    public string? Phone { get; set; }
    public string? Picture { get; set; }
    public string? Siret { get; set; }
    public string? Address { get; set; }
    
}

public class UpdateMairieCommandHandler : IRequestHandler<UpdateMairieCommand, MairieEntity>
{
    private readonly IVilleajourDbContext _context;

    public UpdateMairieCommandHandler(IVilleajourDbContext context)
    {
        this._context = context;
    }

    public async Task<MairieEntity?> Handle(UpdateMairieCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Mairies.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity != null) 
        {
            entity.Phone = request.Phone;
            entity.Picture = request.Picture;
            entity.Siret = request.Siret;
            entity.Address = request.Address;

            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }
        else 
        {
            return null;
        }
    }
}
