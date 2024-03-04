using Grenat.Functional.DDD.Generators.Src.Extensions;

namespace Grenat.Functional.DDD.Generators.Src.Models;

public abstract class CollectionProperty : IProperty
{
    public ISymbol Symbol { get; set; }
    public string FieldName => Symbol.Name;
    public string Accessibility => Symbol.GetAccessibility();
    public abstract string TypeName { get; }
    public TypeData InnerType { get; }
    public ITypeSymbol TypeSymbol { get; }
    public bool DontGenerateSetters { get; private set; }
    public bool IsImmutable { get; set; }

    protected CollectionProperty(
        ISymbol symbol,
        ITypeSymbol typeSymbol,
        TypeData innerType,
        bool dontGenerateSetters
        )
    {
        Symbol = symbol;
        InnerType = innerType;
        DontGenerateSetters = dontGenerateSetters;
        TypeSymbol = typeSymbol;
        IsImmutable = typeSymbol.Name.StartsWith("Immutable");
    }

}
