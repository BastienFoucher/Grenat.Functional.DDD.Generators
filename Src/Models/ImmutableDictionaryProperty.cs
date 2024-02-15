namespace Grenat.Functional.DDD.Generators.Src.Models;

public sealed class ImmutableDictionaryProperty : CollectionProperty, IEquatable<ImmutableDictionaryProperty>
{
    public override string TypeName => $"ImmutableDictionary<{KeyType.TypeName}, {InnerType.TypeName}>";
    public IType KeyType { get; set; } 

    public ImmutableDictionaryProperty(
        string fieldName, 
        IType keyTypeName,
        IType innerType,
        bool dontGenerateSetters)
        : base(fieldName, innerType, dontGenerateSetters)
    {
        KeyType = keyTypeName;
    }

    #region IEquatable
    public override bool Equals(object obj)
    {
        return Equals(obj as ImmutableDictionaryProperty);
    }

    public bool Equals(ImmutableDictionaryProperty other)
    {
        return other is not null &&
               FieldName == other.FieldName &&
               TypeName == other.TypeName &&
               DontGenerateSetters == other.DontGenerateSetters &&
               KeyType == other.KeyType;
    }

    public override int GetHashCode()
    {
        int hashCode = -1238536815;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FieldName);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TypeName);
        hashCode = hashCode * -1521134295 + EqualityComparer<IType>.Default.GetHashCode(KeyType);
        hashCode = hashCode * -1521134295 + EqualityComparer<IType>.Default.GetHashCode(InnerType);
        hashCode = hashCode * -1521134295 + DontGenerateSetters.GetHashCode();
        return hashCode;
    }
    #endregion
}