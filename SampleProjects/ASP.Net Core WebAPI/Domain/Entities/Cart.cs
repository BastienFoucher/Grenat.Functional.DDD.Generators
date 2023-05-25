namespace SampleProject.Domain.Entities;

[Entity, GenerateSetters, GenerateBuilder, GenerateDefaultConstructor]
public partial record Cart
{
    public Identifier Id { get; init; }
    public ImmutableDictionary<string, CartItem> Items { get; init; }
    public Amount TotalAmount { get; init; }

    [StaticConstructor]
    public static Entity<Cart> Create(string idValue, ImmutableDictionary<string, Entity<CartItem>> items, int totalAmountValue, string totalAmountCurrency)
    {
        return Entity<Cart>.Valid(new Cart())
            .SetId(idValue)
            .SetItems(items)
            .SetTotalAmount(totalAmountValue, totalAmountCurrency);
    }
}
