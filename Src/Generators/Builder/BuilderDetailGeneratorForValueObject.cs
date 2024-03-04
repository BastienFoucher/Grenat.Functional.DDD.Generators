using Grenat.Functional.DDD.Generators.Src.Extensions;

namespace Grenat.Functional.DDD.Generators.Src.Generators.Builder;

internal class BuilderDetailGeneratorForValueObject : BuilderDetailGenerator
{
    private ValueObjectProperty ValueObjectProperty { get; }

    public BuilderDetailGeneratorForValueObject(
        IProperty property,
        string parentClassOrRecordName,
        string builderName)
        : base(property, parentClassOrRecordName, builderName)
    {
        ValueObjectProperty = (ValueObjectProperty)property;
    }

    public override (StringBuilder, ImmutableList<string>) Generate()
    {
        var result = new StringBuilder();
        var setterParametersList = new StringBuilder();
        var setterArgumentsList = new StringBuilder();
        var generatedPrivateFields = ImmutableList<string>.Empty;
        var parentProperty = (IProperty)ValueObjectProperty;

        // Looping over ValueObjects members
        foreach (var field in ValueObjectProperty.Fields)
        {
            var paramName = $"{field.GetPrivateBuilderFieldName(parentProperty).Remove(0, 1)}";
            var privateBuilderFieldName = field.GetPrivateBuilderFieldName(parentProperty);

            result.Append($@"
    private {field.TypeName} {privateBuilderFieldName} {{ get; set; }}");
            setterParametersList.Append($"{field.TypeName} {paramName}, ");
            setterArgumentsList.Append($@"
        {privateBuilderFieldName} = {paramName};");
            generatedPrivateFields = generatedPrivateFields.Add(privateBuilderFieldName);
        }

        generatedPrivateFields = generatedPrivateFields.Add(this.GetPrivateBuilderFieldName());

        result.Append($@"
    public {BuilderName} With{Property.FieldName}({setterParametersList.ToString().RemoveLastChars(2)})
    {{{setterArgumentsList}
        return this;
    }}

    private {Property.TypeName} {this.GetPrivateBuilderFieldName()} {{ get; set; }}
    public {BuilderName} With{ValueObjectProperty.Symbol}({ValueObjectProperty.TypeName} {Property.FieldName.ToLowerFirstChar()})
    {{
        {this.GetPrivateBuilderFieldName()} = {Property.FieldName.ToLowerFirstChar()};
        return this;
    }}
");

        return (result, generatedPrivateFields);
    }
}
