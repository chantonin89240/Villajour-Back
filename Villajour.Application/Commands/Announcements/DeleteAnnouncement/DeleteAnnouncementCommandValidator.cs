using FluentValidation;

namespace Villajour.Application.Commands.Announcements.DeleteAnnouncement
{
    public class DeleteAnnouncementCommandValidator : AbstractValidator<DeleteAnnouncementCommand>
    {
        public DeleteAnnouncementCommandValidator()
        {
            RuleFor(a => a.Id).NotEmpty();
        }
    }
}
