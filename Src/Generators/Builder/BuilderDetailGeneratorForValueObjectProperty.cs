using Grenat.Functional.DDD.Generators.Src.Extensions;

namespace Grenat.Functional.DDD.Generators.Src.Generators.Builder;

internal class BuilderDetailGeneratorForValueObjectProperty : BuilderDetailGenerator
{
    private ValueObjectProperty ValueObjectProperty { get; }

    public BuilderDetailGeneratorForValueObjectProperty(
        IProperty property,
        string parentClassOrRecordName)
        : base(property, parentClassOrRecordName)
    {
        ValueObjectProperty = (ValueObjectProperty)property;
    }

    public override (StringBuilder, ImmutableList<string>) Generate()
    {
        var result = new StringBuilder();
        var setterParametersList = new StringBuilder();
        var setterArgumentsList = new StringBuilder();
        var generatedPrivateFields = ImmutableList<string>.Empty;

        // Looping over ValueObjects members
        foreach (var field in ValueObjectProperty.Fields)
        {
            var paramName = $"{field.GetPrivateBuilderFieldName().Remove(0, 1)}";

            result.Append($@"
    private {field.TypeName} {field.GetPrivateBuilderFieldName()} {{ get; set; }}");
            setterParametersList.Append($"{field.TypeName} {paramName}, ");
            setterArgumentsList.Append($@"
    {field.GetPrivateBuilderFieldName()} = {paramName};");
            generatedPrivateFields = generatedPrivateFields.Add(field.GetPrivateBuilderFieldName());
        }

        generatedPrivateFields = generatedPrivateFields.Add(this.GetPrivateBuilderFieldName());

        result.Append($@"
    public {ParentSymbolName} With{Property.FieldName}({setterParametersList.ToString().RemoveLastChars(2)})
    {{{setterArgumentsList}
        return this;
    }}

    private ValueObject<{Property.TypeName}> {this.GetPrivateBuilderFieldName()} {{ get; set; }}
    public {ParentSymbolName} With{ValueObjectProperty.FieldName}({ValueObjectProperty.TypeName}<{ValueObjectProperty.InnerType.TypeName}> {Property.FieldName.ToLowerFirstChar()})
    {{
        {this.GetPrivateBuilderFieldName()} = {Property.FieldName.ToLowerFirstChar()};
        return this;
    }}
");

        return (result, generatedPrivateFields);
    }
}
