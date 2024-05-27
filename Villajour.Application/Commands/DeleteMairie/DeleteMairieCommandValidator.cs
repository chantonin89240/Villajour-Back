using FluentValidation;

namespace Villajour.Application.Commands.DeleteMairie;

public class deleteMairieCommandValidator : AbstractValidator<DeleteMairieCommand>
{
    public deleteMairieCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
