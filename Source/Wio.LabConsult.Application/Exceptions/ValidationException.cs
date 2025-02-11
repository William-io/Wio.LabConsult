using FluentValidation.Results;

namespace Wio.LabConsult.Application.Exceptions;

public class ValidationException : ApplicationException
{
    public IDictionary<string, string[]>? Errors { get; }

    public ValidationException() : base("Ocorreram uma ou mais falhas de validação.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures
                    .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                    .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}
