namespace Grenat.Functional.DDD.Generators.Interfaces;

public interface IProperty : IType
{
    public ISymbol Symbol { get; }
    public string FieldName { get; }
    public string Accessibility { get; }
    public bool DontGenerateSetters { get; }
}
