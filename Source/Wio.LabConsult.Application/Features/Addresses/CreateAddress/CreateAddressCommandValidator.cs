using FluentValidation;

namespace Wio.LabConsult.Application.Features.Addresses.CreateAddress;

public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressCommandValidator()
    {
        RuleFor(p => p.Country)
            .NotEmpty().WithMessage("");

        RuleFor(p => p.State)
            .NotEmpty().WithMessage("");

        RuleFor(p => p.ZipCode)
            .NotEmpty().WithMessage("");

        RuleFor(p => p.City)
            .NotEmpty().WithMessage("");

        RuleFor(p => p.Street)
            .NotEmpty().WithMessage("");
    }
}

