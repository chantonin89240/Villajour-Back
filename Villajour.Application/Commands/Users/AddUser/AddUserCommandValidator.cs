using FluentValidation;

namespace Villajour.Application.Commands.Users.AddUser;

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Phone).Matches("^[0-9]+$").MaximumLength(10);
    }
}
