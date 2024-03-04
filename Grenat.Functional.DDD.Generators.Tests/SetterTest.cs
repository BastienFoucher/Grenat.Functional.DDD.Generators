namespace Grenat.Functional.DDD.Generators.Tests;

public class SettersTest : TestBase
{
    [Fact]
    public Task Verify_Option_Setter()
    {
        // The source code to test
        var source = $@"
{GetHeadersCode()}

{GetCartItemEntityCode()}
{GetCouponCode()}

{GenerateSettersAttribute()}
public partial record Cart
{{
    public Option<int> CouponId {{ get; private set; }}
    public Option<Coupon> Coupon {{ get; private set; }}
}}
";

        return VerifyGeneratedCode(source);
    }

    [Fact]
    public Task Verify_Immutable_Collection_Setter()
    {
        // The source code to test
        var source = $@"
{GetHeadersCode()}

{GetCartItemEntityCode()}

{GenerateSettersAttribute()}
public partial record Cart
{{
    public ImmutableList<CartItem> CartItems {{ get; private set; }}
}}
";

        return VerifyGeneratedCode(source);
    }

    [Fact]
    public Task Verify_Record_Dictionary_Setter()
    {
        // The source code to test
        var source = $@"
{GetHeadersCode()}

{GetCartItemEntityCode()}

{GenerateSettersAttribute()}
public partial record Cart
{{
    public ImmutableDictionary<Code, CartItem> ImmutableCartItems {{ get; private set; }}
    public Dictionary<Code, CartItem> CartItems {{ get; private set; }}
}}
";

        return VerifyGeneratedCode(source);
    }

    [Fact]
    public Task Verify_Record_Entity_ValueObject_Value_Setter()
    {
        // The source code to test
        var source = $@"
{GetHeadersCode()}

{GetCartItemEntityCode()}

{GenerateSettersAttribute()}
public partial record Cart
{{
    public int Id {{ get; private set; }}
    public Code Code {{ get; private set; }}
    public CartItem CartItem {{ get; private set; }}
}}
";

        return VerifyGeneratedCode(source);
    }
}