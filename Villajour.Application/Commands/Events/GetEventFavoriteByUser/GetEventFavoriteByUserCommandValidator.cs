using FluentValidation;

namespace Villajour.Application.Commands.Events.GetEventFavoriteByUser;

public class GetEventFavoriteByUserCommandValidator : AbstractValidator<GetEventFavoriteByUserCommand>
{
    public GetEventFavoriteByUserCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
    }
}