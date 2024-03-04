namespace Grenat.Functional.DDD.Generators.Src.Generators.DefaultConstructor;

internal abstract class SetterDetailGenerator
{
    protected IProperty Property { get; }
    protected string ParentSymbolName { get; }

    internal SetterDetailGenerator(IProperty property, string parentClassOrRecordName)
    {
        Property = property;
        ParentSymbolName = parentClassOrRecordName;
    }

    public abstract StringBuilder Generate();
}
