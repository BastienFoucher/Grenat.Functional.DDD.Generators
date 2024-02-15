namespace Grenat.Functional.DDD.Generators.Tests;

public class DefaultConstructorTest : TestBase
{
    [Fact]
    public Task Verify_Default_Constructor()
    {
        // The source code to test
        var source = $@"
{GetHeadersCode()}

{GetCartItemEntityCode()}

{GenerateDefaultConstructorAttribute()}
public partial class Cart
{{
    public int Id {{ get; private set; }}
    public List<int> Ids {{ get; private set; }}
    public Option<int> CouponId {{ get; private set; }}
    public CartItem Item {{ get; private set; }}
    public ImmutableList<CartItem> Items {{ get; private set;}}
    public ImmutableHashSet<CartItem> ItemsHashSet {{ get; private set;}}
    public ImmutableDictionary<int, CartItem> ItemsDictionary {{ get; private set;}}
}}
";

        return VerifyGeneratedCode(source);
    }
}