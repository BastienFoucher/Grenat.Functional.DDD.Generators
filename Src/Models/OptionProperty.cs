namespace Grenat.Functional.DDD.Generators.Src.Models;

public sealed class OptionProperty : DddProperty, IEquatable<OptionProperty>
{
    public override string TypeName => $"Option<{InnerType.TypeName}>";

    public OptionProperty(
        string fieldName,
        ITypeSymbol typeSymbol,
        TypeData innerType,
        bool hasDefaultConstructor,
        bool dontGenerateSetters)
        : base(fieldName, typeSymbol, innerType, hasDefaultConstructor, dontGenerateSetters)
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
               FieldName == other.FieldName &&
               TypeName == other.TypeName &&
               InnerType == other.InnerType &&
               DontGenerateSetters == other.DontGenerateSetters;
    }

    public override int GetHashCode()
    {
        int hashCode = -1238536815;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FieldName);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TypeName);
        hashCode = hashCode * -1521134295 + EqualityComparer<IType>.Default.GetHashCode(InnerType);
        hashCode = hashCode * -1521134295 + DontGenerateSetters.GetHashCode();
        return hashCode;
    }
    #endregion
}