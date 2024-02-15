//HintName: BuilderGeneratorTests.Cart.g.cs
//generation count: 1
namespace BuilderGeneratorTests;
    
public partial class CartBuilder
{
    private List<int> _collection { get; set; }
    public Cart WithCollection(List<int> collection)
    {
        _collection = collection;
        return this;
    }

    public  Build() => ();

}