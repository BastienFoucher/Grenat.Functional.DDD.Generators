//HintName: BuilderGeneratorTests.Cart.g.cs
//generation count: 1
namespace BuilderGeneratorTests;
    
public partial record CartBuilder
{
    private Int32 _id { get; set; }
    public Cart WithId(Int32 id)
    {
        _id = id;
        return this;
    }

    private List<int> _collection { get; set; }
    public Cart WithCollection(List<int> collection)
    {
        _collection = collection;
        return this;
    }

    public  Build() => ();

}