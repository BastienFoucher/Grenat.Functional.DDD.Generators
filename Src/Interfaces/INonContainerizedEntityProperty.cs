namespace Grenat.Functional.DDD.Generators.Interfaces;

public interface INonContainerizedEntityProperty : IEntityProperty
{
    public bool HasDefaultConstructor { get; }
}
