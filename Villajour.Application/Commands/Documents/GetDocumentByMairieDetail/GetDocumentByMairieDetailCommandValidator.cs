using FluentValidation;

namespace Villajour.Application.Commands.Documents.GetDocumentByMairieDetail;

public class GetDocumentByMairieDetailCommandValidator : AbstractValidator<GetDocumentByMairieDetailCommand>
{
    public GetDocumentByMairieDetailCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.MairieId).NotEmpty();
    }
}
