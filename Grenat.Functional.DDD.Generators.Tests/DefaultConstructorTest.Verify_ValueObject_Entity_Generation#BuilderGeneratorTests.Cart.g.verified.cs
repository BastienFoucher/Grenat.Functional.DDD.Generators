//HintName: BuilderGeneratorTests.Cart.g.cs
//generation count: 1
namespace BuilderGeneratorTests;
    
public class CartBuilder
{
    private Entity<CartItem> _cartItem { get; set; }
    public Cart WithCartItem(Entity<CartItem> cartItem)
    {
        _cartItem = cartItem;
        return this;
    }

    public  Build() => ();

}