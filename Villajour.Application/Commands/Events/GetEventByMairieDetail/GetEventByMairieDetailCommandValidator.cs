using FluentValidation;

namespace Villajour.Application.Commands.Events.GetEventByMairieDetail;

public class GetEventByMairieDetailCommandValidator : AbstractValidator<GetEventByMairieDetailCommand>
{
    public GetEventByMairieDetailCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.MairieId).NotEmpty();
    }
}
