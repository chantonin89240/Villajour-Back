using FluentValidation;

namespace Villajour.Application.Commands.Events.GetEventByMairie;

public class GetEventByMairieCommandValidator : AbstractValidator<GetEventByMairieCommand>
{
    public GetEventByMairieCommandValidator()
    {
        RuleFor(c => c.MairieId).NotEmpty();
    }
}