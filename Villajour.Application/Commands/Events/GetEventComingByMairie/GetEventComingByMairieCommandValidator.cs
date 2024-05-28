using FluentValidation;

namespace Villajour.Application.Commands.Events.GetEventComingByMairie;

public class GetEventComingByMairieCommandValidator : AbstractValidator<GetEventComingByMairieCommand>
{
    public GetEventComingByMairieCommandValidator()
    {
        RuleFor(c => c.MairieId).NotEmpty();
    }
}