using FluentValidation;

namespace Wio.LabConsult.Application.Features.Auth.Users.Commands.UpdateAdminUser;

public class UpdateAdminUserCommandValidator : AbstractValidator<UpdateAdminUserCommand>
{
    public UpdateAdminUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome não pode ser vazio");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Sobrenome não pode ser vazio");
        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Telefone não pode ser vazio");
    }
}