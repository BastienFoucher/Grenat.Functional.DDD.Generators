namespace Grenat.Functional.DDD.Generators.Interfaces;

public interface INonContainerizedDddProperty : IDddProperty
{
    public bool HasDefaultConstructor { get; }
}
