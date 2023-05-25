namespace SampleProject.Domain.Entities;

[Entity, GenerateSetters, GenerateBuilder, GenerateDefaultConstructor]
public partial record CartItem
{
    public Identifier Id { get; init; }
    public Identifier CartId { get; init; }
    public Identifier ProductId { get; init; }
    public Amount Amount { get; init; }

    [StaticConstructor]
    public static Entity<CartItem> Create(string idValue, string cartIdValue, string productIdValue, int amountValue, string amountCurrency)
    {
        return Entity<CartItem>.Valid(new CartItem())
            .SetId(idValue)
            .SetCartId(cartIdValue)
            .SetProductId(productIdValue)
            .SetAmount(amountValue, amountCurrency);
    }
}

