using FluentValidation;

namespace Wio.LabConsult.Application.Features.Auth.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Nome não pode ser nulo");
        RuleFor(p => p.LastName)
            .NotEmpty().WithMessage("Nome não pode ser nulo");
    }
}
