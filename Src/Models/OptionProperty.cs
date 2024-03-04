namespace Grenat.Functional.DDD.Generators.Src.Models;

public sealed class OptionProperty : DddProperty, IEquatable<OptionProperty>
{
    public override string TypeName => $"Option<{InnerType.TypeName}>";

    public OptionProperty(
        ISymbol Symbol,
        ITypeSymbol typeSymbol,
        TypeData innerType,
        bool hasDefaultConstructor,
        bool dontGenerateSetters)
        : base(Symbol, typeSymbol, innerType, hasDefaultConstructor, dontGenerateSetters)
    {
    }

    #region IEquatable
    public override bool Equals(object obj)
    {
        return Equals(obj as OptionProperty);
    }

    public bool Equals(OptionProperty other)
    {
        return other is not null &&
               Symbol.Equals(other.Symbol, SymbolEqualityComparer.Default) &&
               TypeName == other.TypeName &&
               InnerType == other.InnerType &&
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