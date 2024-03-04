//HintName: BuilderGeneratorTests.Cart.g.cs
//generation count: 1
namespace BuilderGeneratorTests;
public static partial record CartSetters
{
    public static Cart SetCartItems(this Cart cart, ImmutableDictionary<Code, CartItem> cartItems)
    {
        return cart with { cartItems = cartItems };
    }

    public static Entity<Cart> SetCartItems(this Cart cart, ImmutableDictionary<Code, Entity<CartItem>> cartItems)
    {
        return cart.SetDictionary(cartItems, static (cart, cartItems) => cart with { CartItems = cartItems });
    }

    public static Entity<Cart> SetCartItems(this Entity<Cart> cart, ImmutableDictionary<Code, Entity<CartItem>> cartItems)
    {
        return cart.SetDictionary(cartItems, static (cart, cartItems) => cart with { CartItems = cartItems });
    }

}