namespace Grenat.Functional.DDD.Generators.Models;

public sealed class ValueObjectMember : IProperty, IEquatable<ValueObjectMember>
{
    public ValueObjectMember(string name, string type, string parentFieldName, bool dontGenerateSetters)
    {
        FieldName = name;
        Type = type;
        ParentFieldName = parentFieldName;
        DontGenerateSetters = dontGenerateSetters;
    }

    public string FieldName { get; }
    public string Type { get; }
    public string ParentFieldName { get; }

    public bool DontGenerateSetters { get; }

    public StringBuilder GenerateSetters(string recordName, string varName)
    {
        throw new NotImplementedException();
    }

    #region IEquatable
    public override bool Equals(object obj)
    {
        return Equals(obj as ValueObjectMember);
    }

    public bool Equals(ValueObjectMember other)
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

    public static bool operator ==(ValueObjectMember left, ValueObjectMember right)
    {
        return EqualityComparer<ValueObjectMember>.Default.Equals(left, right);
    }

    public static bool operator !=(ValueObjectMember left, ValueObjectMember right)
    {
        return !(left == right);
    }
    #endregion
}
