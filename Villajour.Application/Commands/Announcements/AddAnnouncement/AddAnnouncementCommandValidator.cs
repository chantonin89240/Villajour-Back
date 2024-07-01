using FluentValidation;

namespace Villajour.Application.Commands.Announcements.AddAnnouncement;

public class AddAnnouncementCommandValidator : AbstractValidator<AddAnnouncementCommand>
{
    public AddAnnouncementCommandValidator()
    {
        RuleFor(a => a.Title).NotEmpty();
        RuleFor(a => a.Description).NotEmpty();
        RuleFor(a => a.AnnouncementTypeId).NotEmpty();
        RuleFor(a => a.MairieId).NotEmpty();
    }

}
