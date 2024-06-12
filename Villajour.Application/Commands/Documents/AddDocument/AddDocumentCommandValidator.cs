using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Villajour.Application.Commands.Documents.AddDocument;

public class AddDocumentCommandValidator : AbstractValidator<AddDocumentCommand>
{
    public AddDocumentCommandValidator()
    {
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.DocumentUrl).NotEmpty();
        RuleFor(c => c.DocumentTypeId).NotEmpty();
        RuleFor(c => c.MairieId).NotEmpty();
    }
}
