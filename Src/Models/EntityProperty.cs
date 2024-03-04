namespace Grenat.Functional.DDD.Generators.Src.Models;

public sealed class EntityProperty : DddProperty, IEquatable<EntityProperty>
{
    public override string TypeName => $"Entity<{InnerType.TypeName}>";

    public EntityProperty(
        ISymbol symbol, 
        ITypeSymbol typeSymbol,
        TypeData innerType,
        bool hasDefaultConstructor, 
        bool dontGenerateSetters)
        : base(symbol, typeSymbol, innerType, hasDefaultConstructor, dontGenerateSetters)
    {
    }

    #region IEquatable
    public override bool Equals(object obj)
    {
        return Equals(obj as EntityProperty);
    }

    public bool Equals(EntityProperty other)
    {
        return other is not null &&
               Symbol.Equals(other.Symbol, SymbolEqualityComparer.Default) &&
               TypeName == other.TypeName &&
               DontGenerateSetters == other.DontGenerateSetters;
    }

    public override int GetHashCode()
    {
        int hashCode = -1238536815;
        hashCode = hashCode * -1521134295 + SymbolEqualityComparer.Default.GetHashCode(Symbol);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TypeName);
        hashCode = hashCode * -1521134295 + EqualityComparer<IType>.Default.GetHashCode(InnerType);
        hashCode = hashCode * -1521134295 + DontGenerateSetters.GetHashCode();
        return hashCode;
    }
    #endregion
}
