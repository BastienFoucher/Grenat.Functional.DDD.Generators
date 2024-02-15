namespace Grenat.Functional.DDD.Generators.Src.Generators.DefaultConstructor;

internal class DefaultConstructorDetailGenerator
{
    protected IProperty Property { get; set; }
    protected string ParentSymbolName { get; set; }

    internal DefaultConstructorDetailGenerator(IProperty property, string parentClassOrRecordName)
    {
        Property = property;
        ParentSymbolName = parentClassOrRecordName;
    }

    public virtual StringBuilder Generate()
    {
        return new StringBuilder().Append($@"
        {Property.FieldName} = new();");
    }
}
