//HintName: BuilderGeneratorTests.Cart.g.cs
//generation count: 1
namespace BuilderGeneratorTests;
public static class CartSetters
{
    public static Entity<Cart> SetCartItems(this Cart cart, ICollection<Entity<CartItem>> cartItems)
    {
        return cart.SetCollection(cartItems, static (cart, cartItems) => cart with { CartItems = cartItems });
    }

    public static Entity<Cart> SetCartItems(this Entity<Cart> cart, ICollection<Entity<CartItem>> cartItems)
    {
        return cart.SetCollection(cartItems, static (cart, cartItems) => cart with { CartItems = cartItems });
    }

}