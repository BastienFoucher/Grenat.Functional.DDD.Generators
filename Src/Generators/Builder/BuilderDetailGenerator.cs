using Grenat.Functional.DDD.Generators.Src.Extensions;

namespace Grenat.Functional.DDD.Generators.Src.Generators.Builder;

internal class BuilderDetailGenerator
{
    protected IProperty Property { get; }
    protected string ParentSymbolName { get; }
    protected string BuilderName {  get; }

    internal BuilderDetailGenerator(IProperty property, string parentClassOrRecordName, string builderName)
    {
        Property = property;
        ParentSymbolName = parentClassOrRecordName;
        BuilderName = builderName;
    }

    public virtual (StringBuilder, ImmutableList<string>) Generate()
    {
        var generatedPrivateFields = ImmutableList<string>.Empty;
        generatedPrivateFields = generatedPrivateFields.Add(GetPrivateBuilderFieldName());

        return (new StringBuilder().Append($@"
    private {Property.TypeName} {this.GetPrivateBuilderFieldName()} {{ get; set; }}
    public {BuilderName} With{Property.FieldName}({Property.TypeName} {Property.FieldName.ToLowerFirstChar()})
    {{
        {GetPrivateBuilderFieldName()} = {Property.FieldName.ToLowerFirstChar()};
        return this;
    }}
"), generatedPrivateFields);
    }

    protected string GetPrivateBuilderFieldName()
    {
        return $"_{Property.FieldName.ToLowerFirstChar()}";
    }
}
