namespace SampleProject.Entities;

[Entity, GenerateSetters, GenerateBuilder, GenerateDefaultConstructor]
public partial record Cart
{
    public Identifier Id { get; init; }
    public ImmutableList<CartItem> Items { get; init; }
    public Amount TotalAmount { get; init; }

    [StaticConstructor]
    public static Entity<Cart> Create(string idValue, ImmutableList<Entity<CartItem>> items, int totalAmountValue, string totalAmountCurrency)
    {
        return Entity<Cart>.Valid(new Cart())
            .SetId(idValue)
            .SetItems(items)
            .SetTotalAmount(totalAmountValue, totalAmountCurrency);
    }
}
