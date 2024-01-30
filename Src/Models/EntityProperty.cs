namespace Grenat.Functional.DDD.Generators.Src.Models;

public sealed class EntityProperty : DddProperty, IEquatable<EntityProperty>
{
    public override string TypeName => $"Entity<{InnerType.TypeName}>";

    public EntityProperty(
        string fieldName, 
        IType innerType,
        bool hasDefaultConstructor, 
        bool dontGenerateSetters)
        : base(fieldName, innerType, hasDefaultConstructor, dontGenerateSetters)
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
               FieldName == other.FieldName &&
               TypeName == other.TypeName &&
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
