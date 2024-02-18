namespace Grenat.Functional.DDD.Generators.Src.Generators.DefaultConstructor;

internal abstract class SetterDetailGenerator
{
    protected IProperty Property { get; set; }
    protected string ParentSymbolName { get; set; }

    internal SetterDetailGenerator(IProperty property, string parentClassOrRecordName)
    {
        Property = property;
        ParentSymbolName = parentClassOrRecordName;
    }

    public abstract StringBuilder Generate();
}
