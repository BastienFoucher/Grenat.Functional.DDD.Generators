namespace SampleProject.ValueObjects;

[ValueObject]
public record Identifier
{
    public const string DEFAULT_VALUE = "";
    public const int MAX_LENGTH = 10;

    [Value]
    public readonly string Value = "";

    public Identifier() { Value = DEFAULT_VALUE; }

    private Identifier(string identifier)
    {
        Value = identifier;
    }

    public static ValueObject<Identifier> Create(string identifier)
    {
        if (string.IsNullOrEmpty(identifier))
            return new Error("An identifier cannot be null or empty.");

        else if (identifier.Length > MAX_LENGTH)
            return new Error($"Identifier {identifier} cannot be longer than {MAX_LENGTH} characters.");

        return new Identifier(identifier);
    }
}
