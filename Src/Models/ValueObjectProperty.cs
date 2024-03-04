using Grenat.Functional.DDD.Generators.Src.Models;

namespace Grenat.Functional.DDD.Generators.Models;

public sealed class ValueObjectProperty : DddProperty, IEquatable<ValueObjectProperty>
{
    public override string TypeName => $"ValueObject<{InnerType.TypeName}>";
    public IEnumerable<IProperty> Fields { get; }

    public ValueObjectProperty(
        ISymbol symbol,
        ITypeSymbol typeSymbol,
        TypeData innerType,
        IEnumerable<IProperty> valueObjectFields,
        bool hasDefaultConstructor,
        bool dontGenerateSetters)
        : base(symbol, typeSymbol, innerType, hasDefaultConstructor, dontGenerateSetters)
    {
        Fields = valueObjectFields;
    }

public override bool Equals(object obj)
    {
        return Equals(obj as ValueObjectProperty);
    }

    public bool Equals(ValueObjectProperty other)
    {
        return other is not null &&
               Symbol.Equals(other.Symbol, SymbolEqualityComparer.Default) &&
               TypeName == other.TypeName &&
               DontGenerateSetters == other.DontGenerateSetters &&
               Fields.SequenceEqual(other.Fields) &&
               InnerType == other.InnerType;
    }

    public override int GetHashCode()
    {
        int hashCode = -793926097;
        hashCode = hashCode * -1521134295 + SymbolEqualityComparer.Default.GetHashCode(Symbol);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TypeName);
        hashCode = hashCode * -1521134295 + DontGenerateSetters.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<IType>.Default.GetHashCode(InnerType);
        hashCode = hashCode * -1521134295 + EqualityComparer<IEnumerable<IProperty>>.Default.GetHashCode(Fields);
        return hashCode;
    }

    #region IEquatable

    #endregion
}
