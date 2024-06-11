using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villajour.Application.Commands.Documents.GetDocumentHistoByMairie;

namespace Villajour.Application.Commands.Documents.GetDocumentType;

public class GetDocumentTypeCommandValidator : AbstractValidator<GetDocumentTypeCommand>
{
    public GetDocumentTypeCommandValidator()
    {
       
    }
}