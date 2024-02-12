namespace Grenat.Functional.DDD.Generators.Tests;

public class BuilderGeneratorTest : TestBase
{
    [Fact]
    public Task Verify_Value_Fields_Generation_For_Class()
    {
        // The source code to test
        var source = $@"
{GetHeadersCode()}

{GetAllGrenatDddAttributesCode()}
public partial class Cart
{{
    public int Id {{ get; private set; }}
    public List<int> Collection {{ get; private set;}}
}}
";

        return VerifyGeneratedCode(source);
    }

    [Fact]
    public Task Verify_Value_Fields_Generation_For_Record()
    {
        // The source code to test
        var source = $@"
{GetHeadersCode()}

{GetAllGrenatDddAttributesCode()}
public partial record Cart
{{
    public int Id {{ get; private set; }}
    public List<int> Collection {{ get; private set;}}
}}
";

        return VerifyGeneratedCode(source);
    }

    [Fact]
    public Task Verify_Inherited_Fields_Generation()
    {
        // The source code to test
        var source = $@"
{GetHeadersCode()}

public record EntityBase
{{
    public int Id {{ get; private set; }}
}}

{GetAllGrenatDddAttributesCode()}
public partial record Cart : EntityBase
{{
    public List<int> Collection {{ get; private set;}}
}}
";

        return VerifyGeneratedCode(source);
    }

    [Fact]
    public Task Verify_ValueObject_Fields_Generation()
    {
        // The source code to test
        var source = $@"
{GetHeadersCode()}

{GetAmountValueObjectCode()}

{GetIdentifierValueObjectCode()}

{GetAllGrenatDddAttributesCode()}
public partial class Cart
{{
    public Identifier Id {{ get; private set; }}
    public Amount TotalAmount {{ get; set; }}
}}
";

        return VerifyGeneratedCode(source);
    }

    [Fact]
    public Task Verify_ValueObject_Entity_Generation()
    {
        // The source code to test
        var source = $@"
{GetHeadersCode()}

{GetAmountValueObjectCode()}

{GetIdentifierValueObjectCode()}

{GetCartItemEntityCode()}

{GetAllGrenatDddAttributesCode()}
public partial class Cart
{{
    public CartItem CartItem {{get; set;}}
}}
";

        return VerifyGeneratedCode(source);
    }

    [Fact]
    public Task Verify_Option_Generation()
    {
        // The source code to test
        var source = $@"
{GetHeadersCode()}

{GetAmountValueObjectCode()}

{GetIdentifierValueObjectCode()}

{GetCartItemEntityCode()}

{GetAllGrenatDddAttributesCode()}
public partial class Cart
{{
    public Option<CartItem> Item {{get; set;}}
    public Option<Code> Coupon {{get; set;}}
    public Option<int> OptionableInt {{get; set;}}
}}
";

        return VerifyGeneratedCode(source);
    }

    [Fact]
    public Task Verify_List_Entity_Generation()
    {
        // The source code to test
        var source = $@"
{GetHeadersCode()}

{GetAmountValueObjectCode()}

{GetIdentifierValueObjectCode()}

{GetAllGrenatDddAttributesCode()}
public partial class Cart
{{
    public List<CartItem> Items {{get; set;}}
}}
";

        return VerifyGeneratedCode(source);
    }

    [Fact]
    public Task Verify_Dictionary_Entity_Generation()
    {
        // The source code to test
        var source = $@"
{GetHeadersCode()}

{GetAmountValueObjectCode()}

{GetIdentifierValueObjectCode()}

{GetAllGrenatDddAttributesCode()}
public partial class Cart
{{
    public Dictionary<int, CartItem> Items {{get; set;}}
}}
";

        return VerifyGeneratedCode(source);
    }

    [Fact]
    public Task Verify_BuildMethod()
    {
        // The source code to test
        var source = $@"
{GetHeadersCode()}

{GetAmountValueObjectCode()}

{GetIdentifierValueObjectCode()}

{GetAllGrenatDddAttributesCode()}
public partial class Cart
{{
    public int Id {{get; set;}}
    public Amount TotalAmount {{get; set;}}
    public Option<Code> Coupon {{get; set;}}
    public ImmutableList<CartItem> Items {{get; set;}}

    [StaticConstructor]
    public Entity<Cart> Create(int id, string totalAmountCurrency, int totalAmountValue, Option<Code> coupon) 
    {{
        Id = id;
        TotalAmount = totalAmount;
        Coupon = coupon;
        Items = ImmutableList<CartItem>.Empty;
    }}
}}
";

        return VerifyGeneratedCode(source);
    }
}