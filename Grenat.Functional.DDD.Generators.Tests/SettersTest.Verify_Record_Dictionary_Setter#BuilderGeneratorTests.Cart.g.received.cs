//HintName: BuilderGeneratorTests.Cart.g.cs
//generation count: 1
namespace BuilderGeneratorTests;
public static class CartSetters
{
    public static Entity<Cart> SetImmutableCartItems(this Cart cart, ImmutableDictionary<Code, Entity<CartItem>> immutableCartItems)
    {
        return cart.SetDictionary(immutableCartItems, static (cart, immutableCartItems) => cart with { ImmutableCartItems = immutableCartItems });
    }

    public static Entity<Cart> SetImmutableCartItems(this Entity<Cart> cart, ImmutableDictionary<Code, Entity<CartItem>> immutableCartItems)
    {
        return cart.SetDictionary(immutableCartItems, static (cart, immutableCartItems) => cart with { ImmutableCartItems = immutableCartItems });
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