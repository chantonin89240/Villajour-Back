using FluentValidation;

namespace Villajour.Application.Commands.Announcements.GetAnnoucementByFavoriteMaire
{
    public class GetAnnouncementByFavoriteMairieCommandValidator : AbstractValidator<GetAnnoucementByFavoriteMaireCommand>
    {
        public GetAnnouncementByFavoriteMairieCommandValidator()
        {
            RuleFor(c => c.UserId).NotEmpty();
        }
    }
}
