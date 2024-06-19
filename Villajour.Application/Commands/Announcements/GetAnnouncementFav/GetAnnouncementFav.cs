using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Announcements.GetAnnouncementFav
{
    public class GetAnnouncementFavCommand : IRequest<List<AnnouncementEntity>>
    {
        public Guid UserId { get; set; }
    }

    public class GetAnnouncementFavHandler : IRequestHandler<GetAnnouncementFavCommand, List<AnnouncementEntity>>
    {
        private readonly IVillajourDbContext _context;

        public GetAnnouncementFavHandler(IVillajourDbContext context)
        {
            _context = context;
        }

        public async Task<List<AnnouncementEntity?>> Handle(GetAnnouncementFavCommand request, CancellationToken cancellationToken)
        {
            List<AnnouncementEntity> entity = await _context.Announcements
                 .Where(e => _context.FavoritesContent
                     .Where(f => f.UserId == request.UserId && f.AnnouncementId.HasValue)
                     .Select(f => f.AnnouncementId.Value)
                     .Contains(e.Id))
                 .OrderBy(e => e.Date)
                 .ToListAsync(cancellationToken);


            return entity;
        }
    }
}
