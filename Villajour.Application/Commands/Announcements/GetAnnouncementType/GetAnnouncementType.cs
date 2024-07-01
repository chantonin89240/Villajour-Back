using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Announcements.GetAnnouncementType;

public record class GetAnnouncementTypeCommand : IRequest<List<AnnouncementTypeEntity>>
{

}

public class GetAnnouncementTypeHandler : IRequestHandler<GetAnnouncementTypeCommand, List<AnnouncementTypeEntity>>
{
    private readonly IVillajourDbContext _context;

    public GetAnnouncementTypeHandler(IVillajourDbContext context)
    {
        _context = context;
    }

    public async Task<List<AnnouncementTypeEntity?>> Handle(GetAnnouncementTypeCommand request, CancellationToken cancellationToken)
    {
        List<AnnouncementTypeEntity> entity = await _context.AnnouncementTypes.ToListAsync(cancellationToken);

        return entity;
    }
}