namespace SampleProject.ValueObjects;

[ValueObject]
public record Amount
{
    public const int MAX_AMOUNT = 2500;
    public const int DEFAULT_VALUE = 0;
    public const string DEFAULT_CURRENCY = "EUR";

    [Value]
    public readonly int Value;

    [Value]
    public readonly string Currency = DEFAULT_CURRENCY;

    public Amount() { Value = DEFAULT_VALUE; }

    private Amount(int value, string currency = DEFAULT_CURRENCY)
    {
        Value = value;
        Currency = currency;
    }

    public static ValueObject<Amount> Create(int amount, string currency)
    {
        if (amount < 0)
            return new Error("An amount cannot be negative.");
        if (amount > MAX_AMOUNT)
            return new Error(String.Format($"The {amount} {currency} exceeds the {MAX_AMOUNT} {currency} max value."));

        return new Amount(amount, currency);
    }

    public static implicit operator int(Amount amount) => amount.Value;
}
