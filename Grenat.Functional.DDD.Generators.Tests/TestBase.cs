using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;

namespace Grenat.Functional.DDD.Generators.Tests;

public class TestBase
{
    public Task VerifyGeneratedCode(string source)
    {
        // Parse the provided string into a C# syntax tree
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);

        // Create a Roslyn compilation for the syntax tree.
        CSharpCompilation compilation = CSharpCompilation.Create(
            assemblyName: "Tests",
            syntaxTrees: new[] { syntaxTree });


        // Create an instance of our EnumGenerator incremental source generator
        var generator = new DddGenerator();

        // The GeneratorDriver is used to run our generator against a compilation
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        // Run the source generator!
        driver = driver.RunGenerators(compilation);

        // Use verify to snapshot test the source generator output!
        return Verifier.Verify(driver);
    }

    public string GetHeadersCode()
    {
        return @"
using Grenat.Functional.DDD;
using Grenat.Functional.DDD.Generators;

namespace BuilderGeneratorTests;
";
    }

    public string GenerateBuilderAttribute()
    {
        return @"[Entity,GenerateBuilder]";
    }

    public string GenerateDefaultConstructorAttribute()
    {
        return @"[Entity,GenerateDefaultConstructor]";
    }

    public string GetAmountValueObjectCode()
    {
        return @"

[ValueObject]
public record Amount
{
    public const int MAX_AMOUNT = 2500;
    public const int DEFAULT_VALUE = 0;
    public const string DEFAULT_CURRENCY = ""EUR"";

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
            return new Error(""An amount cannot be negative."");
        if (amount > MAX_AMOUNT)
            return new Error(String.Format($""The {amount} {currency} exceeds the {MAX_AMOUNT} {currency} max value.""));

        return new Amount(amount, currency);
    }

    public static implicit operator int(Amount amount) => amount.Value;
}
";
    }

    public string GetIdentifierValueObjectCode()
    {
        return @"

[ValueObject]
public record Identifier
{
    public const string DEFAULT_VALUE = """";
    public const int MAX_LENGTH = 10;

    [Value]
    public readonly string Value = """";

    public Identifier() { Value = DEFAULT_VALUE; }

    private Identifier(string identifier)
    {
        Value = identifier;
    }

    public static ValueObject<Identifier> Create(string identifier)
    {
        if (string.IsNullOrEmpty(identifier))
            return new Error(""An identifier cannot be null or empty."");

        else if (identifier.Length > MAX_LENGTH)
            return new Error($""Identifier {identifier} cannot be longer than {MAX_LENGTH} characters."");

        return new Identifier(identifier);
    }
}
";
    }

    public string GetCartItemEntityCode()
    {
        return @"
[Entity]
public partial class CartItem
{
    public Identifier Id { get; private set; }
    public Identifier ProductId { get; init; }
    public Amount Amount { get; init; }
}";
    }
}
