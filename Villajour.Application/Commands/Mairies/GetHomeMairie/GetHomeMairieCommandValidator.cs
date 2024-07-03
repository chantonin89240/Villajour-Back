using FluentValidation;

namespace Villajour.Application.Commands.Mairies.GetHomeMairie;

public class GetHomeMairieCommandValidator : AbstractValidator<GetHomeMairieCommand>
{
    public GetHomeMairieCommandValidator()
    {
        RuleFor(c => c.MairieId).NotEmpty();
    }
}