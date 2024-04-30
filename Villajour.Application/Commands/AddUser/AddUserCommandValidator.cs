using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villajour.Application.Commands.AddUser;

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator()
    {
        RuleFor(b => b.Name).NotEmpty();
        RuleFor(b => b.Email).NotEmpty();
        RuleFor(b => b.Status).NotEmpty();
        RuleFor(b => b.City).NotEmpty();
        RuleFor(b => b.PostalCode).NotEmpty();
        RuleFor(b => b.Country).NotEmpty();
        RuleFor(b => b.Address).NotEmpty();
        RuleFor(b => b.Phone).NotEmpty();
    }
}
