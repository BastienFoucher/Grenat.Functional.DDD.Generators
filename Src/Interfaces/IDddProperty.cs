namespace Grenat.Functional.DDD.Generators.Interfaces;

public interface IDddProperty : IProperty, IEquatable<IDddProperty>
{
    public bool DontGenerateSetters { get; }

    public StringBuilder GenerateSetters(string recordName, string varName);
    public (StringBuilder, ImmutableList<string>) GenerateBuilderDetails(string recordName);
    public StringBuilder GenerateDefaultConstructorDetail();
}

public enum BaseDddPropertyType
{
    Unknown,
    ValueObject,
    Entity,
    ContainerizedDddProperty
}
