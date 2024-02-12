namespace Grenat.Functional.DDD.Generators.Src.Models;

public sealed class ImmutableCollectionProperty : CollectionProperty, IEquatable<ImmutableCollectionProperty>
{
    public override string TypeName => $"ImmutableList<{InnerType.TypeName}>";

    public ImmutableCollectionProperty(
        string fieldName, 
        IType innerType, 
        bool dontGenerateSetters)
        : base(fieldName, innerType, dontGenerateSetters)
    {
    }

    #region IEquatable
    public override bool Equals(object obj)
    {
        return Equals(obj as ImmutableCollectionProperty);
    }

    public bool Equals(ImmutableCollectionProperty other)
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