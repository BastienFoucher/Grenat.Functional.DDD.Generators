namespace Grenat.Functional.DDD.Generators.Src.Models;

public sealed class DictionaryProperty : CollectionProperty, IEquatable<DictionaryProperty>
{
    public override string TypeName => IsImmutable switch
    {
        true => $"ImmutableDictionary<{KeyType.TypeName}, {InnerType.TypeName}>",
        _ => $"Dictionary<{KeyType.TypeName}, {InnerType.TypeName}>"
    };

    public IType KeyType { get; set; } 

    public DictionaryProperty(
        ISymbol symbol,
        ITypeSymbol typeSymbol,
        IType keyTypeName,
        TypeData innerType,
        bool dontGenerateSetters)
        : base(symbol, typeSymbol, innerType, dontGenerateSetters)
    {
        IsImmutable = typeSymbol.Name.Contains("Immutable");
        KeyType = keyTypeName;
    }

    #region IEquatable
    public override bool Equals(object obj)
    {
        return Equals(obj as DictionaryProperty);
    }

    public bool Equals(DictionaryProperty other)
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