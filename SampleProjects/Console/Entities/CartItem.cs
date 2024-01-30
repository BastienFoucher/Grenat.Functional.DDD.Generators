namespace SampleProject.Entities;

[Entity, GenerateBuilder, GenerateSetters, GenerateDefaultConstructor]
public partial class CartItem
{
    public Identifier Id { get; private set; }
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
