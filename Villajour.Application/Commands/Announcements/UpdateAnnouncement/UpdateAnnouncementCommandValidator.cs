using FluentValidation;

namespace Villajour.Application.Commands.Announcements.UpdateAnnouncement
{
    public class UpdateAnnouncementCommandValidator : AbstractValidator<UpdateAnnouncementCommand>
    {
        public UpdateAnnouncementCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.Title).NotEmpty();
            RuleFor(c => c.MairieId).NotEmpty().Must(value => value is Guid).WithMessage("La valeur de la propriété doit être de type Guid.");
        }
    }
}
