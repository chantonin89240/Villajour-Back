using FluentValidation;

namespace Villajour.Application.Commands.Announcements.GetAnnouncementHistoByMairie;

public class GetAnnouncementHistoByMairieCommandValidator : AbstractValidator<GetAnnouncementHistoByMairieCommand>
{
    public GetAnnouncementHistoByMairieCommandValidator()
    {
        RuleFor(c => c.MairieId).NotEmpty();
    }
}
