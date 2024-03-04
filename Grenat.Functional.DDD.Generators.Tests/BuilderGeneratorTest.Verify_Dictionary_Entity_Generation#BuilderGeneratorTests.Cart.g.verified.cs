//HintName: BuilderGeneratorTests.Cart.g.cs
//generation count: 1
namespace BuilderGeneratorTests;
    
public class CartBuilder
{
    private Dictionary<Int32, CartItem> _items { get; set; }
    public Cart WithItems(Dictionary<Int32, CartItem> items)
    {
        _items = items;
        return this;
    }

    public  Build() => ();

}