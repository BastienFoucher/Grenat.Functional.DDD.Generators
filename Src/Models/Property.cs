namespace Grenat.Functional.DDD.Generators.Models;

public class Property : IProperty
{
    public Property(string name, string type)
    {
        FieldName = name;
        Type = type;
    }

    public string FieldName { get; }
    public string Type { get; }
}

public static class PropertyExtensions
{
    public static string GetPrivateBuilderFieldName(this IProperty property)
    {
        if (property is ValueObjectProperty valueObjectProperty)
            return $"_{valueObjectProperty.ParentFieldName.ToLowerFirstChar()}{property.FieldName.ToUpperFirstChar()}";

        else
            return $"_{property.FieldName.ToLowerFirstChar()}";
    }
}
