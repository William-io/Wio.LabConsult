using FluentValidation;

namespace Wio.LabConsult.Application.Features.Auth.Users.Commands.LoginUser;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("{PropertyName} Email não pode ser null.");
        RuleFor(p => p.Password)
            .NotEmpty().WithMessage("{PropertyName} Senha não pode ser null.");
    }
}
