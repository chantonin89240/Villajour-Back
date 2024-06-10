using FluentValidation;
namespace Villajour.Application.Commands.Documents.GetDocumentFav;

public class GetDocumentFavCommandValidator : AbstractValidator<GetDocumentFavCommand>
{
    public GetDocumentFavCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
    }
}