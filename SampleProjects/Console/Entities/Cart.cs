namespace SampleProject.Entities;

[Entity, GenerateSetters, GenerateBuilder, GenerateDefaultConstructor]
public partial class Cart
{
    public int Coincoin { get; init; }
    public Identifier Id { get; private set; }
    public CartItem MainItem { get; private set; }
    public ImmutableList<CartItem> Items { get; init; }
    public Amount TotalAmount { get; set; }

    [StaticConstructor]
    public static Entity<Cart> Create(int coincoin, string idValue, ImmutableList<Entity<CartItem>> items, int totalAmountValue, string totalAmountCurrency)
    {
        return Entity<Cart>.Valid(new Cart())
            .SetCoincoin(coincoin)
            .SetId(idValue)
            .SetItems(items)
            .SetTotalAmount(totalAmountValue, totalAmountCurrency);
    }
}
