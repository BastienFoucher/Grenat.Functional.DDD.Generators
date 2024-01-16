namespace Grenat.Functional.DDD.Generators.Interfaces;

public interface IProperty
{
    public string FieldName { get; }
    public string Type { get; }
    public bool DontGenerateSetters { get; }
}
