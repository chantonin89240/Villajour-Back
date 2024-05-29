using FluentValidation;

namespace Villajour.Application.Commands.Mairies.DeleteMairie;

public class DeleteMairieCommandValidator : AbstractValidator<DeleteMairieCommand>
{
    public DeleteMairieCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
