# Grenat.Functional.DDD.Generators
Some Roslyn C# incremental source generators to generate **Setters**, **Builders** and **Default constructors** [patterns](https://github.com/BastienFoucher/Grenat.Functional.DDD#some-patterns) for the [Grenat.Functional.DDD library](https://github.com/BastienFoucher/Grenat.Functional.DDD) that I use for a personal project.

Development is in progress and some improvements will be done like generating Warnings if inconsistencies are detected in the code or to generate class mapping to an `Entity`.


## Grenat.Functional.DDD

Have a look at the [Readme file of this library](https://github.com/BastienFoucher/Grenat.Functional.DDD#readme) first to understand its philosophy.

## Sample projects

Have a look at the [sample projects](https://github.com/BastienFoucher/Grenat.Functional.DDD.Generators/tree/main/SampleProjects) that are included in the solution.

## Defining Value Objects

- Use the `[ValueObject]` to define a `Record` (and only a record) like a Value Object.
- Use `[Value]` to tell the generator that a property is a Value. A Value Object can have more than one `[Value]` attribute.
- Define your Value Object as a `partial` record.

For example:

```C#
[ValueObject]
public record Identifier
{
    public const string DEFAULT_VALUE = "";
    public const int MAX_LENGTH = 5;

    [Value]
    public readonly string Value = "";


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
```

## Defining Entities

Use the `[Entity]` attribute to define a Record (and only Records) as an Entity and make your record `partial`.

## Generating Entity Builders

- Ask the generator to generate a builder with the `[GenerateBuilder]` attribute.
- Create a static constructor and mark it with the `[StaticConstructor]` attribute.

The builders will be generated using the [Grenat.Functional.DDD guidelines](https://github.com/BastienFoucher/Grenat.Functional.DDD#the-problem-of-constructors).


```C#
[Entity, GenerateBuilder]
public partial record CartItem
{
    public Identifier Id { get; init; }
    public Identifier ProductId { get; init; }
    public Amount Amount { get; init; }

    [StaticConstructor]
    public static Entity<CartItem> Create(string idValue, string productIdValue, int amountValue, string amountCurrency)
    {
        return Entity<CartItem>.Valid(new CartItem())
            .SetId(idValue)
            .SetProductId(productIdValue)
            .SetAmount(amountValue, amountCurrency);
    }
}
```

You can then use the generated builder this way:
```C#
var cartItem = new CartItemBuilder()
    .WithId("45xxsDg1=")
    .WithProductId("ne252TJqAWk3")
    .WithAmount(25, "EUR")
    .Build())
```

Functions are also provided to build Entities containing...
- Other entities.
- Immutable list of entities.
- Immutable dictionary of entities.

```C#
var cart = new CartBuilder()
    .WithId("1ds3d!bM")
    .WithItems(ImmutableList<Entity<CartItem>>.Empty
        .Add(
            new CartItemBuilder()
            .WithId("45xxsDg1=")
            .WithProductId("ne252TJqAWk3")
            .WithAmount(25, "EUR")
            .Build())
        .Add(
            new CartItemBuilder()
            .WithId("784dfg1=")
            .WithProductId("s4ysc9DneP8")
            .WithAmount(50, "EUR")
            .Build()))
    .WithTotalAmount(75, "EUR")
    .Build()
```

## Generating Entity Default constructors

Use the `GenerateDefaultConstructor` attribute. For it to work, you will need to ensure that every Entity and Value Object have a public default constructor. Those for entities can be built using this code generator but you will have to define default Value Object constructors yourself in order to define their default inner values.

```C#
[Entity, GenerateDefaultConstructor]
public partial record Cart
{
    public Identifier Id { get; init; }
    public ImmutableList<CartItem> Items { get; init; }
    public Amount TotalAmount { get; init; }
}
```

The following default constructor will be generated:
```C#
public Cart() 
{
    Id = new Identifier();
    Items = ImmutableList<CartItem>.Empty;
    TotalAmount = new Amount();
}
```

## Generating Entity Setters

Use the `[GenerateSetters]` to generate ubiquituous langage setters of an Entity using the [Grenat.Functional.DDD guidelines](https://github.com/BastienFoucher/Grenat.Functional.DDD#creating-ubiquitous-language-setters). 

Be careful, Setters can be an anti-pattern that lead to data inconsistency. Use them wisely. You can also use the `[NoSetter]` attribute to tell the generator not to generate a setter for a specific property.

For the following Entity:

```C#
[Entity, GenerateSetters]
public partial record CartItem
{
    public Identifier Id { get; init; }
    public Identifier ProductId { get; init; }
    [NoSetter]
    public Amount Amount { get; init; }
}
```

The following setters will be generated:
```C#
public static partial class CartItemSetters
{
    public static Entity<CartItem> SetId(this Entity<CartItem> cartItem, String value)
    {
        return cartItem.SetId(Identifier.Create(value));
    }

    public static Entity<CartItem> SetId(this Entity<CartItem> cartItem, ValueObject<Identifier> id)
    {
        return cartItem.SetValueObject(id, static (cartItem, id) => cartItem with { Id = id });
    }
```

The generator also works for Entities containing:
- Other entities.
- Immutable list of entities.
- Immutable dictionary of entities.
