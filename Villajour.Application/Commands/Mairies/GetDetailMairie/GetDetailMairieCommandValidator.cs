using FluentValidation;

namespace Villajour.Application.Commands.Mairies.GetDetailMairie;

public class GetDetailMairieCommandValidator : AbstractValidator<GetDetailMairieCommand>
{
    public GetDetailMairieCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.MairieId).NotEmpty();
    }
}