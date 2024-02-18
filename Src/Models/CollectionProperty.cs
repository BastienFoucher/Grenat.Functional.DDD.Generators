namespace Grenat.Functional.DDD.Generators.Src.Models;

public abstract class CollectionProperty : IProperty
{
    public string FieldName { get; private set; }
    public abstract string TypeName { get; }
    public TypeData InnerType { get; }
    public ITypeSymbol TypeSymbol { get; }
    public bool DontGenerateSetters { get; private set; }

    protected CollectionProperty(
        string fieldName,
        ITypeSymbol typeSymbol,
        TypeData innerType,
        bool dontGenerateSetters
        )
    {
        FieldName = fieldName;
        InnerType = innerType;
        DontGenerateSetters = dontGenerateSetters;
        TypeSymbol = typeSymbol;
    }

}
