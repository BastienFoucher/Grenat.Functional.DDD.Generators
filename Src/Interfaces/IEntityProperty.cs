namespace Grenat.Functional.DDD.Generators.Interfaces;

public interface IEntityProperty : IProperty, IEquatable<IEntityProperty>
{
    public StringBuilder GenerateSetters(string recordName, string varName);

    public StringBuilder GenerateDefaultConstructorDetail();

    public (StringBuilder, ImmutableList<string>) GenerateBuilderDetails(string recordName);
}

public enum BaseEntityPropertyType
{
    Unknown,
    ValueObject,
    Entity,
    ContainerizedEntityProperty
}
