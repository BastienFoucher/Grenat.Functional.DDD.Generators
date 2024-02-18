using Grenat.Functional.DDD.Generators.Src.Extensions;

namespace Grenat.Functional.DDD.Generators.Src.Models;

public abstract class DddProperty : IProperty
{
    public string FieldName { get;}
    public abstract string TypeName { get;}
    public TypeData InnerType { get; }
    public ITypeSymbol TypeSymbol { get; }
    public bool DontGenerateSetters { get;}
    public bool HasDefaultConstructor { get; }
    

    protected DddProperty(
        string fieldName,
        ITypeSymbol typeSymbol,
        TypeData innerType,
        bool hasDefaultConstructor, 
        bool dontGenerateSetters)
    {
        FieldName = fieldName;
        InnerType = innerType; 
        DontGenerateSetters = dontGenerateSetters;
        HasDefaultConstructor = hasDefaultConstructor;
        TypeSymbol = typeSymbol;
    }
}
