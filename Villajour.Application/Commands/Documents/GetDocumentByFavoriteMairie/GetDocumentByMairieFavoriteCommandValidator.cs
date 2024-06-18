using FluentValidation;

namespace Villajour.Application.Commands.Documents.GetDocumentByFavoriteMairie;


public class GetDocumentByMairieFavoriteCommandValidator : AbstractValidator<GetDocumentByMairieFavoriteCommand>
{
    public GetDocumentByMairieFavoriteCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
    }
}
