//HintName: BuilderGeneratorTests.Cart.g.cs
//generation count: 1
namespace BuilderGeneratorTests;
    
public class CartBuilder
{
    private Int32 _id { get; set; }
    public CartBuilder WithId(Int32 id)
    {
        _id = id;
        return this;
    }

    private Int32 _totalAmountValue { get; set; }
    private String _totalAmountCurrency { get; set; }
    public CartBuilder WithTotalAmount(Int32 totalAmountValue, String totalAmountCurrency)
    {
        _totalAmountValue = totalAmountValue;
        _totalAmountCurrency = totalAmountCurrency;
        return this;
    }

    private ValueObject<Amount> _totalAmount { get; set; }
    public CartBuilder WithBuilderGeneratorTests.Cart.TotalAmount(ValueObject<Amount> totalAmount)
    {
        _totalAmount = totalAmount;
        return this;
    }

    private Option<Code> _coupon { get; set; }
    public CartBuilder WithCoupon(Option<Code> coupon)
    {
        _coupon = coupon;
        return this;
    }

    private ImmutableList<CartItem> _items { get; set; }
    public CartBuilder WithItems(ImmutableList<CartItem> items)
    {
        _items = items;
        return this;
    }

    public Entity<Cart> Build() => Cart.Create(_id, _totalAmountCurrency, _totalAmountValue, _coupon);

}