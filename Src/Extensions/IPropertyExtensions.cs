namespace Grenat.Functional.DDD.Generators.Src.Extensions;

public static class IPropertyExtensions
{
    public static string GetPrivateBuilderFieldName(this IProperty property)
    {
        //if (property is ValueObjectProperty valueObjectProperty)
        //    return $"_{valueObjectProperty.FieldName.ToLowerFirstChar()}{property.FieldName.ToUpperFirstChar()}";

        //else
            return $"_{property.FieldName.ToLowerFirstChar()}";
    }
}
