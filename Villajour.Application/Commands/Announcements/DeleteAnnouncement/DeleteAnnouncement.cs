using MediatR;
using Microsoft.EntityFrameworkCore;
using Villajour.Application.Commands.Interface;


namespace Villajour.Application.Commands.Announcements.DeleteAnnouncement
{
    public record class DeleteAnnouncementCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
    public class DeleteAnnouncementCommandHandler : IRequestHandler<DeleteAnnouncementCommand, bool>
    {
        private readonly IVillajourDbContext _context;

        public DeleteAnnouncementCommandHandler(IVillajourDbContext context)
        {
            _context = context;
        }

        public class DeleteEntity
        {
            public bool ConfirmationDelete { get; set; }
        }

        public async Task<bool> Handle(DeleteAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Announcements.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity != null)
            {
                _context.Announcements.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
