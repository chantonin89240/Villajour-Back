using FluentValidation;

namespace Villajour.Application.Commands.Events.GetEventByMairieFavorite;

public class GetEventByMairieFavoriteCommandValidator : AbstractValidator<GetEventByMairieFavoriteCommand>
{
    public GetEventByMairieFavoriteCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
    }

}


