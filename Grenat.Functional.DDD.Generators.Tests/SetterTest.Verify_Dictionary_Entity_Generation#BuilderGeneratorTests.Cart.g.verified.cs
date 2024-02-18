//HintName: BuilderGeneratorTests.Cart.g.cs
//generation count: 1
namespace BuilderGeneratorTests;
    
public class CartBuilder
{
    private Dictionary<int, CartItem> _items { get; set; }
    public Cart WithItems(Dictionary<int, CartItem> items)
    {
        _items = items;
        return this;
    }

    public  Build() => ();

}