using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villajour.Application.Commands.Events.GetEventByMairieFavorite;

namespace Villajour.Application.Commands.Announcements.GetAnnouncementByMairieFavorite;

public class GetAnnouncementByMairieFavoriteCommandValidator : AbstractValidator<GetAnnouncementByMairieFavoriteCommand>
{
    public GetAnnouncementByMairieFavoriteCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
    }

}
