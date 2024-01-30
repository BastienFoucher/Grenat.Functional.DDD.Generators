using Grenat.Functional.DDD.Generators.Src.Models;

namespace Grenat.Functional.DDD.Generators.Models;

public sealed class ValueProperty : IProperty, IEquatable<ValueProperty>
{ 
    public string FieldName { get; private set; }

    public string TypeName {get; private set;}

    public bool DontGenerateSetters { get; set; }

    public ValueProperty(string fieldName, string typeName, bool dontGenerateSetters)
    {
        FieldName = fieldName;
        TypeName = typeName;
        DontGenerateSetters = dontGenerateSetters;
    }

    #region IEquatable
    public override bool Equals(object obj)
    {
        return Equals(obj as ValueProperty);
    }

    public bool Equals(ValueProperty other)
    {
        return other is not null &&
               FieldName == other.FieldName &&
               TypeName == other.TypeName &&
               DontGenerateSetters == other.DontGenerateSetters;
    }

    public override int GetHashCode()
    {
        int hashCode = 1977427925;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FieldName);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TypeName);
        hashCode = hashCode * -1521134295 + DontGenerateSetters.GetHashCode();
        return hashCode;
    }
    #endregion
}
