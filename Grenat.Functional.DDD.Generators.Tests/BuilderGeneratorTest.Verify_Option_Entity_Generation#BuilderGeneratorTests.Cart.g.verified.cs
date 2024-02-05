//HintName: BuilderGeneratorTests.Cart.g.cs
//generation count: 1
namespace BuilderGeneratorTests;
    
public partial class CartBuilder
{
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