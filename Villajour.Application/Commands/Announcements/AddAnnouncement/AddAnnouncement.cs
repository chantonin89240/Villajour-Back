using MediatR;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Announcements.AddAnnouncement
{
    public record class AddAnnouncementCommand : IRequest<AnnouncementEntity>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int AnnoucementId { get; set; }
        public Guid MairieId { get; set; }
    }
    public class AddAnnouncementCommandHandler : IRequestHandler<AddAnnouncementCommand, AnnouncementEntity>
    {
        private readonly IVillajourDbContext _context;

        public AddAnnouncementCommandHandler(IVillajourDbContext context)
        {
            _context = context;
        }

        public async Task<AnnouncementEntity> Handle(AddAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var entity = new AnnouncementEntity
            {
                Title = request.Title,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Description = request.Description,
                AnnouncementTypeId = request.AnnoucementId,
                MairieId = request.MairieId
            };

            _context.Announcements.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity;

        }
    }
}
