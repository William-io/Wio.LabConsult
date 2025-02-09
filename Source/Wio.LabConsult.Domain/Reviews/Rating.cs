namespace Wio.LabConsult.Domain.Reviews;

public sealed record Rating
{
    private Rating(int value) => Value = value;
    public int Value { get; }

    public static Rating From(int value)
    {
        if (value < 1 || value > 5)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "A classificação deve estar entre 1 e 5.");
        }
        return new Rating(value);
    }
}
