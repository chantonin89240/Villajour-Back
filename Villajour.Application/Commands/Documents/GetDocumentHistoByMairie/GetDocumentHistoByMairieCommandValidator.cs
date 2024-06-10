using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villajour.Application.Commands.Documents.GetDocumentFav;

namespace Villajour.Application.Commands.Documents.GetDocumentHistoByMairie;

public class GetDocumentHistoByMairieCommandValidator : AbstractValidator<GetDocumentHistoByMairieCommand>
{
    public GetDocumentHistoByMairieCommandValidator()
    {
        RuleFor(c => c.MairieId).NotEmpty();
    }
}
