using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;


namespace Villajour.Application.Commands.Announcements.UpdateAnnouncement
{
    public class UpdateAnnouncementCommand : IRequest<AnnouncementEntity>
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateOnly Date { get; set; }
        public int AnnouncementTypeID { get; set; }
        public Guid MairieId { get; set; }
    }
    public class UpdateAnnouncementCommandHandler : IRequestHandler<UpdateAnnouncementCommand, AnnouncementEntity>
    {
        private readonly IVillajourDbContext _context;

        public UpdateAnnouncementCommandHandler(IVillajourDbContext context)
        {
            _context = context;
        }

        public async Task<AnnouncementEntity?> Handle(UpdateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Announcements.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity != null)
            {
                entity.Title = request.Title;
                entity.Description = request.Description;
                entity.AnnouncementTypeId = request.AnnouncementTypeID;
                entity.Date = DateOnly.FromDateTime(DateTime.Now);
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
}
