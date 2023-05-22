namespace Grenat.Functional.DDD.Generators.Interfaces;

public interface IContainerizedDddProperty : IDddProperty
{
    public INonContainerizedDddProperty InnerDddProperty { get; }
}
