//HintName: BuilderGeneratorTests.Cart.g.cs
//generation count: 1
namespace BuilderGeneratorTests;
    
public class CartBuilder
{
    private List<CartItem> _items { get; set; }
    public Cart WithItems(List<CartItem> items)
    {
        _items = items;
        return this;
    }

    public  Build() => ();

}