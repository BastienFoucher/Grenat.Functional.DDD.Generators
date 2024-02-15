//HintName: BuilderGeneratorTests.Cart.g.cs
//generation count: 1
namespace BuilderGeneratorTests;
    
public class CartBuilder
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

    private Option<Int32> _optionableInt { get; set; }
    public Cart WithOptionableInt(Option<Int32> optionableInt)
    {
        _optionableInt = optionableInt;
        return this;
    }

    public  Build() => ();

}