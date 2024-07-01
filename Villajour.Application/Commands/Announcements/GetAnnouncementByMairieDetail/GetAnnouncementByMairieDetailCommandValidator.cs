using FluentValidation;

namespace Villajour.Application.Commands.Announcements.GetAnnouncementByMairieDetail;

public class GetAnnouncementByMairieDetailCommandValidator : AbstractValidator<GetAnnouncementByMairieDetailCommand>
{
    public GetAnnouncementByMairieDetailCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.MairieId).NotEmpty();
    }
}