﻿using FluentValidation;

namespace Wio.LabConsult.Application.Features.Consults.Commands.CreateConsult;

public class CreateConsultCommandValidator : AbstractValidator<CreateConsultCommand>
{
    public CreateConsultCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Nome não pode ser nulo")
            .MaximumLength(50).WithMessage("Nome não pode ultrapassar 50 caracteres");

        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("Descrição não pode ser nulo");

        RuleFor(p => p.Availability)
            .NotEmpty().WithMessage("Disponibilidade não pode ser nulo");

        RuleFor(p => p.Price)
            .NotEmpty().WithMessage("Preço não pode ser nulo");
    }
}