using FluentValidation;

namespace Villajour.Application.Commands.AddUser;

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Phone).Matches("^[0-9]+$").MaximumLength(10);
    }
}
