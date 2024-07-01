using FluentValidation;

namespace Villajour.Application.Commands.Announcements.GetAnnouncementFavoriteByUser;

public class GetAnnouncementFavoriteByUserCommandValidator : AbstractValidator<GetAnnouncementFavoriteByUserCommand>
{
    public GetAnnouncementFavoriteByUserCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
    }
}