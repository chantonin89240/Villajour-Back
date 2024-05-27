using FluentValidation;

namespace Villajour.Application.Commands.DeleteMairie;

public class DeleteUserCommandValidator : AbstractValidator<DeleteMairieCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
