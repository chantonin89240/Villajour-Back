using FluentValidation;

namespace Villajour.Application.Commands.Events.DeleteEvent;

public class DeleteEventCommandValidator : AbstractValidator<DeleteEventCommand>
{
    public DeleteEventCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
