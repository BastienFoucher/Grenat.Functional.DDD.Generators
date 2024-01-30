namespace Grenat.Functional.DDD.Generators.Interfaces;

public interface IProperty : IType
{
    public string FieldName { get; }
    public bool DontGenerateSetters { get; }
}
