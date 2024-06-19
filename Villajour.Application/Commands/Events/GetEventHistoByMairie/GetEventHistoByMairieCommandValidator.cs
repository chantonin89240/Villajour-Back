using FluentValidation;
using Villajour.Application.Commands.Events.GetEventHistoByMairie;

namespace Villajour.Application.Commands.Events.GetEventByMairie;

public class GetEventHistoByMairieCommandValidator : AbstractValidator<GetEventHistoByMairieCommand>
{
    public GetEventHistoByMairieCommandValidator()
    {
        RuleFor(c => c.MairieId).NotEmpty();
    }
}