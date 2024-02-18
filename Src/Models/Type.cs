using Grenat.Functional.DDD.Generators.Src.Extensions;

namespace Grenat.Functional.DDD.Generators.Models;

public record TypeData : IType
{
    public TypeData(ITypeSymbol typeSymbol)
    {
        TypeName = typeSymbol.Name;
        TypeSymbol = typeSymbol;
    }

    public string TypeName { get; }
    public ITypeSymbol TypeSymbol{ get; }

    public string TypeNameWithDddContainer { get => GetTypeNameWithDddContainer(); }

    public string GetTypeNameWithDddContainer()
    {
        if (TypeSymbol.IsEntity())
            return $"Entity<{TypeName}>";
        else if (TypeSymbol.IsValueObject())
            return $"ValueObject<{TypeName}>";
        else
            return TypeName;
    }
}
