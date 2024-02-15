//HintName: BuilderGeneratorTests.Cart.g.cs
//generation count: 1
namespace BuilderGeneratorTests;
public partial class Cart
{
    public Cart() 
    {
        Id = new();
        Ids = new();
        CouponId = None<Int32>();
        Item = new();
        Items = ImmutableList<CartItem>.Empty;
        ItemsHashSet = ImmutableHashSet<CartItem>.Empty;
        ItemsDictionary = ImmutableDictionary<Int32, CartItem>.Empty;
    }
}
        