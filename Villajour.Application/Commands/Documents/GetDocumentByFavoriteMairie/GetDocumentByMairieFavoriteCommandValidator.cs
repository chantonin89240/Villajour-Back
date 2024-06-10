using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villajour.Application.Commands.Documents.DeleteDocument;

namespace Villajour.Application.Commands.Documents.GetDocumentByFavoriteMairie;


public class GetDocumentByMairieFavoriteCommandValidator : AbstractValidator<GetDocumentByMairieFavoriteCommand>
{
    public GetDocumentByMairieFavoriteCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
    }
}
