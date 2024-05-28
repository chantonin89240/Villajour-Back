using FluentValidation;

namespace Villajour.Application.Commands.DeleteEvent;

public class DeleteUserCommandValidator : AbstractValidator<DeleteEventCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
