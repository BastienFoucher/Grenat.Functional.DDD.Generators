using Grenat.Functional.DDD.Generators.Src.Generators;

namespace Grenat.Functional.DDD.Generators.Src.Models;

public record StaticEntityConstructorParameter
{
    public string Name { get; }
    public string Type { get; }

    public StaticEntityConstructorParameter(string name, string type)
    {
        Name = name;
        Type = type;
    }
}

public class StaticConstructor
{
    public bool Any { get; }
    public string Name { get; }
    public string SymbolName { get; }
    public string ReturningType { get; }

    public ImmutableList<StaticEntityConstructorParameter> Parameters { get; }

    public StaticConstructor(
        bool any,
        string symbolName,
        string name,
        ImmutableList<StaticEntityConstructorParameter> parameters,
        string returningType)
    {
        Any = any;
        Name = name;
        SymbolName = symbolName;
        Parameters = parameters;
        ReturningType = returningType;
    }
}
