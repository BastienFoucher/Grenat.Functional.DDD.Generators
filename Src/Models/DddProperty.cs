namespace Grenat.Functional.DDD.Generators.Src.Models;

public abstract class DddProperty : IProperty
{
    public string FieldName { get;}
    public abstract string TypeName { get;}
    public IType InnerType { get; }
    public bool DontGenerateSetters { get;}
    public bool HasDefaultConstructor { get; }

    protected DddProperty(
        string fieldName,
        IType innerType,
        bool hasDefaultConstructor, 
        bool dontGenerateSetters)
    {
        FieldName = fieldName;
        InnerType = innerType; 
        DontGenerateSetters = dontGenerateSetters;
        HasDefaultConstructor = hasDefaultConstructor;
    }

}
