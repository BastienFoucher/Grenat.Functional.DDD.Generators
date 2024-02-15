//HintName: BuilderGeneratorTests.Cart.g.cs
//generation count: 1
namespace BuilderGeneratorTests;
    
public partial class CartBuilder
{
    private Option<CartItem> _item { get; set; }
    public Cart WithItem(Option<CartItem> item)
    {
        _item = item;
        return this;
    }

    private Option<Code> _coupon { get; set; }
    public Cart WithCoupon(Option<Code> coupon)
    {
        _coupon = coupon;
        return this;
    }

    private Option<int> _optionableInt { get; set; }
    public Cart WithOptionableInt(Option<int> optionableInt)
    {
        _optionableInt = optionableInt;
        return this;
    }

    public  Build() => ();

}