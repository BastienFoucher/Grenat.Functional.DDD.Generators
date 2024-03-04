using Grenat.Functional.DDD.Generators.Src.Extensions;

namespace Grenat.Functional.DDD.Generators.Src.Models;

public abstract class DddProperty : IProperty
{
    public ISymbol Symbol { get;}
    public string FieldName => Symbol.Name;
    public string Accessibility => Symbol.GetAccessibility();
    public abstract string TypeName { get;}
    public TypeData InnerType { get; }
    public ITypeSymbol TypeSymbol { get; }
    public bool DontGenerateSetters { get;}
    public bool HasDefaultConstructor { get; }
    

    protected DddProperty(
        ISymbol symbol,
        ITypeSymbol typeSymbol,
        TypeData innerType,
        bool hasDefaultConstructor, 
        bool dontGenerateSetters)
    {
        Symbol = symbol;
        InnerType = innerType; 
        DontGenerateSetters = dontGenerateSetters;
        HasDefaultConstructor = hasDefaultConstructor;
        TypeSymbol = typeSymbol;
    }
}
