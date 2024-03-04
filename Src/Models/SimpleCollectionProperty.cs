namespace Grenat.Functional.DDD.Generators.Src.Models;

public sealed class SimpleCollectionProperty : CollectionProperty
{
    public override string TypeName => IsImmutable switch
    {
        false => 
            IsList 
            ? $"List<{InnerType.TypeName}>"
            : IsHashSet 
                ? $"HashSet<{InnerType.TypeName}>" 
                : $"{InnerType.TypeName}[]",
        true => 
            IsList 
            ? $"ImmutableList<{InnerType.TypeName}>"
            : IsHashSet 
                ? $"ImmutableHashSet<{InnerType.TypeName}>" 
                : $"ImmutableArray<{InnerType.TypeName}>"
    };
    public bool IsList { get; set; }
    public bool IsHashSet { get; set; }
    public bool IsArray { get; set; }

    public SimpleCollectionProperty(
        ISymbol symbol, 
        ITypeSymbol typeSymbol,
        TypeData innerType, 
        bool dontGenerateSetters)
        : base(symbol, typeSymbol, innerType, dontGenerateSetters)
    {
        IsList = typeSymbol.Name.Contains("List");
        IsHashSet = typeSymbol.Name.Contains("HashSet");
        IsArray = typeSymbol.Name.Contains("Array");
    }
}