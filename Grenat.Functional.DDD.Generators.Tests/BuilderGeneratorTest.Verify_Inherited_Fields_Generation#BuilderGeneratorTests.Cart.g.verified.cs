//HintName: BuilderGeneratorTests.Cart.g.cs
//generation count: 1
namespace BuilderGeneratorTests;
    
public record CartBuilder
{
    private Int32 _id { get; set; }
    public Cart WithId(Int32 id)
    {
        _id = id;
        return this;
    }

    private List<Int32> _collection { get; set; }
    public Cart WithCollection(List<Int32> collection)
    {
        _collection = collection;
        return this;
    }

    public  Build() => ();

}