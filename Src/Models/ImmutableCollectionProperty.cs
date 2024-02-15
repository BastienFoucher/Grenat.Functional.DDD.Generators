namespace Grenat.Functional.DDD.Generators.Src.Models;

public abstract class ImmutableCollectionProperty : CollectionProperty
{
    public override string TypeName => $"ImmutableList<{InnerType.TypeName}>";

    public ImmutableCollectionProperty(
        string fieldName, 
        IType innerType, 
        bool dontGenerateSetters)
        : base(fieldName, innerType, dontGenerateSetters)
    {
    }
}