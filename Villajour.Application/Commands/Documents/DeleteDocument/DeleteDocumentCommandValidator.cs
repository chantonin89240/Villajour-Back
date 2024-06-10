using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villajour.Application.Commands.Mairies.DeleteMairie;

namespace Villajour.Application.Commands.Documents.DeleteDocument;

public class DeleteDocumentCommandValidator : AbstractValidator<DeleteDocumentCommand>
{
    public DeleteDocumentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
