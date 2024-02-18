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
}