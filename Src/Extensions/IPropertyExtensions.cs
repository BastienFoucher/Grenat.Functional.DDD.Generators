namespace Grenat.Functional.DDD.Generators.Src.Extensions;

public static class IPropertyExtensions
{
    public static string GetPrivateBuilderFieldName(this IProperty property, IProperty parentProperty)
    {
            return $"_{parentProperty.FieldName.ToLowerFirstChar()}{property.FieldName}";
    }
}
