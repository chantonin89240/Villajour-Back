using FluentValidation;

namespace Villajour.Application.Commands.Announcements.GetAnnouncementFav
{
    public class GetAnnouncementFavCommandValidator : AbstractValidator<GetAnnouncementFavCommand>
    {
        public GetAnnouncementFavCommandValidator()
        {
            RuleFor(c => c.UserId).NotEmpty();
        }
    }
}
