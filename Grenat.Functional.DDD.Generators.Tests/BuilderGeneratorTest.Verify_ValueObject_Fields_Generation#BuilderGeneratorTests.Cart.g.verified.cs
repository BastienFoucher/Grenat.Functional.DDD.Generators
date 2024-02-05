//HintName: BuilderGeneratorTests.Cart.g.cs
//generation count: 1
namespace BuilderGeneratorTests;
    
public partial class CartBuilder
{
    private String _idValue { get; set; }
    public Cart WithId(String idValue)
    {
        _idValue = idValue;
        return this;
    }

    private ValueObject<Identifier> _id { get; set; }
    public Cart WithId(ValueObject<Identifier> id)
    {
        _id = id;
        return this;
    }

    private Int32 _totalAmountValue { get; set; }
    private String _totalAmountCurrency { get; set; }
    public Cart WithTotalAmount(Int32 totalAmountValue, String totalAmountCurrency)
    {
        _totalAmountValue = totalAmountValue;
        _totalAmountCurrency = totalAmountCurrency;
        return this;
    }

    private ValueObject<Amount> _totalAmount { get; set; }
    public Cart WithTotalAmount(ValueObject<Amount> totalAmount)
    {
        _totalAmount = totalAmount;
        return this;
    }

    public  Build() => ();

}