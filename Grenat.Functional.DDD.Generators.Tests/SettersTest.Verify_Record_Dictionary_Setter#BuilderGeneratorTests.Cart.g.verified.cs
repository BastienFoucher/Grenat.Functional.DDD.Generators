//HintName: BuilderGeneratorTests.Cart.g.cs
//generation count: 1
namespace BuilderGeneratorTests;
public static partial record CartSetters
{
    public static Cart SetImmutableCartItems(this Cart cart, ImmutableDictionary<Code, CartItem> immutableCartItems)
    {
        return cart with { immutableCartItems = immutableCartItems };
    }

    public static Entity<Cart> SetImmutableCartItems(this Cart cart, ImmutableDictionary<Code, Entity<CartItem>> immutableCartItems)
    {
        return cart.SetDictionary(immutableCartItems, static (cart, immutableCartItems) => cart with { ImmutableCartItems = immutableCartItems });
    }

    public static Entity<Cart> SetImmutableCartItems(this Entity<Cart> cart, ImmutableDictionary<Code, Entity<CartItem>> immutableCartItems)
    {
        return cart.SetDictionary(immutableCartItems, static (cart, immutableCartItems) => cart with { ImmutableCartItems = immutableCartItems });
    }

    public static Cart SetCartItems(this Cart cart, Dictionary<Code, CartItem> cartItems)
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