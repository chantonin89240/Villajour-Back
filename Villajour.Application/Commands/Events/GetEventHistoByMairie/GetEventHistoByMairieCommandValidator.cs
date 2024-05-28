using FluentValidation;

namespace Villajour.Application.Commands.Events.GetEventHistoByMairie;

public class GetEventHistoByMairieCommandValidator : AbstractValidator<GetEventHistoByMairieCommand>
{
    public GetEventHistoByMairieCommandValidator()
    {
        RuleFor(c => c.MairieId).NotEmpty();
    }
}