using FluentValidation;
using Villajour.Application.Commands.Documents.GetDocumentByFavoriteMairie;

namespace Villajour.Application.Commands.Documents.GetDocumentByMairieDetail;

public class GetDocumentByMairieDetailCommandValidator : AbstractValidator<GetDocumentByMairieDetailCommand>
{
    public GetDocumentByMairieDetailCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.MairieId).NotEmpty();
    }
}
