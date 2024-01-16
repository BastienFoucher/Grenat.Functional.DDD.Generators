namespace Grenat.Functional.DDD.Generators.Interfaces;

public interface IContainerizedEntityProperty : IEntityProperty
{
    public INonContainerizedEntityProperty InnerProperty { get; }
}
