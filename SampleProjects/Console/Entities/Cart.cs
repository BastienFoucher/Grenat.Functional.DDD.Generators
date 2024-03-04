namespace SampleProject.Entities;

[Entity, GenerateSetters, GenerateBuilder, GenerateDefaultConstructor]
public partial record Cart
{
    public int Coincoin { get; init; }
    public Identifier Id { get; init; }
    public CartItem MainItem { get; init; }
    public ICollection<CartItem> Items { get; init; }
    public Amount TotalAmount { get; set; }

    [StaticConstructor]
    public static Entity<Cart> Create(int coincoin, string idValue, ICollection<CartItem> items, int totalAmountValue, string totalAmountCurrency)
    {
        return Entity<Cart>.Valid(new Cart())
            .SetCoincoin(coincoin)
            .SetId(idValue)
            .SetItems(items)
            .SetTotalAmount(totalAmountValue, totalAmountCurrency);
    }
}
