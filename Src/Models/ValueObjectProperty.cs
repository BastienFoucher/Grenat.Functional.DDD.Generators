namespace Grenat.Functional.DDD.Generators.Models;

public sealed class ValueObjectProperty : IProperty, IEquatable<ValueObjectProperty>
{
    public ValueObjectProperty(string name, string type, string parentFieldName)
    {
        FieldName = name;
        Type = type;
        ParentFieldName = parentFieldName;
    }

    public string FieldName { get; }
    public string Type { get; }
    public string ParentFieldName { get; }

    public override bool Equals(object obj)
    {
        return Equals(obj as ValueObjectProperty);
    }

    public bool Equals(ValueObjectProperty other)
    {
        return other is not null &&
               FieldName == other.FieldName &&
               Type == other.Type &&
               ParentFieldName == other.ParentFieldName;
    }

    public override int GetHashCode()
    {
        int hashCode = 1907543013;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FieldName);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ParentFieldName);
        return hashCode;
    }

    public static bool operator ==(ValueObjectProperty left, ValueObjectProperty right)
    {
        return EqualityComparer<ValueObjectProperty>.Default.Equals(left, right);
    }

    public static bool operator !=(ValueObjectProperty left, ValueObjectProperty right)
    {
        return !(left == right);
    }
}
