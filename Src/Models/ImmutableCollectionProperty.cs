namespace Grenat.Functional.DDD.Generators.Src.Models;

public abstract class ImmutableCollectionProperty : CollectionProperty
{
    public override string TypeName => $"ImmutableList<{InnerType.TypeName}>";

    public ImmutableCollectionProperty(
        string fieldName, 
        ITypeSymbol typeSymbol,
        TypeData innerType, 
        bool dontGenerateSetters)
        : base(fieldName, typeSymbol, innerType, dontGenerateSetters)
    {
    }
}