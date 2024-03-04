namespace SampleProject.Entities;

[Entity, GenerateBuilder, GenerateSetters, GenerateDefaultConstructor]
public partial record CartItem
{
    public Identifier Id { get; init; }
    public Identifier ProductId { get; init; }
    public Amount Amount { get; init; }

    [StaticConstructor]
    public static Entity<CartItem> Create(string idValue, string productIdValue, int amountValue, string amountCurrency)
    {
        return Entity<CartItem>.Valid(new CartItem())
            .SetId(Identifier.Create(idValue))
            .SetProductId(Identifier.Create(productIdValue))
            .SetAmount(Amount.Create(amountValue, amountCurrency));
    }

}
