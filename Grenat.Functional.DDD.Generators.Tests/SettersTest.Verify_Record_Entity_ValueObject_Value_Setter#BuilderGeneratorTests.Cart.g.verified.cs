//HintName: BuilderGeneratorTests.Cart.g.cs
//generation count: 1
namespace BuilderGeneratorTests;
public static partial record CartSetters
{
    public static Cart SetId(this Cart cart, Int32 id)
    {
        return cart with { Id = id };
    }

    public static Entity<Cart> SetId(this Cart cart, Int32 id)
    {
        return cart.Set(Id, static (cart, id) => cart with { Id = id });
    }

    public static Entity<Cart> SetId(this Entity<Cart> cart, Int32 id)
    {
        return cart.Set(Id, static (cart, id) => cart with { Id = id });
    }

    public static Cart SetCode(this Cart cart, Code code)
    {
        return cart with { Code = code };
    }

    public static Entity<Cart> SetCode(this Cart cart, Code code)
    {
        return cart.Set(Code, static (cart, code) => cart with { Code = code });
    }

    public static Entity<Cart> SetCode(this Entity<Cart> cart, Code code)
    {
        return cart.Set(Code, static (cart, code) => cart with { Code = code });
    }

    public static Cart SetCartItem(this Cart cart, Entity<CartItem> cartItem)
    {
        return cart with { CartItem = cartItem };
    }

    public static Entity<Cart> SetCartItem(this Cart cart, Entity<CartItem> cartItem)
    {
        return cart.Set(CartItem, static (cart, cartItem) => cart with { CartItem = cartItem });
    }

    public static Entity<Cart> SetCartItem(this Entity<Cart> cart, Entity<CartItem> cartItem)
    {
        return cart.Set(CartItem, static (cart, cartItem) => cart with { CartItem = cartItem });
    }

}