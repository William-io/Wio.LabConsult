using FluentValidation;

namespace Wio.LabConsult.Application.Features.Reviews.Commands.CreateReview;
public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotNull().WithMessage("Nome não pode ser nulo!");

        RuleFor(p => p.Comment)
            .NotNull().WithMessage("Comentario não pode ser nulo");

        RuleFor(p => p.Rating)
        .NotEmpty().WithMessage("Rating não permite valores nulos");
    }
}