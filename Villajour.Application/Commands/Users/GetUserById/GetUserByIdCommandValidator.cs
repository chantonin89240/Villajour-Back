﻿using FluentValidation;

namespace Villajour.Application.Commands.Users.GetUserById;

public class GetUserByIdCommandValidator : AbstractValidator<GetUserByIdCommand>
{
    public GetUserByIdCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
