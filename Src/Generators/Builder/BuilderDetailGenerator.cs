using Grenat.Functional.DDD.Generators.Src.Extensions;

namespace Grenat.Functional.DDD.Generators.Src.Generators.Builder;

internal class BuilderDetailGenerator
{
    protected IProperty Property { get; set; }
    protected string ParentSymbolName { get; set; }

    internal BuilderDetailGenerator(IProperty property, string parentClassOrRecordName)
    {
        Property = property;
        ParentSymbolName = parentClassOrRecordName;
    }

    public virtual (StringBuilder, ImmutableList<string>) Generate()
    {
        var generatedPrivateFields = ImmutableList<string>.Empty;
        generatedPrivateFields = generatedPrivateFields.Add(GetPrivateBuilderFieldName());

        return (new StringBuilder().Append($@"
    private {Property.TypeName} {this.GetPrivateBuilderFieldName()} {{ get; set; }}
    public {ParentSymbolName} With{Property.FieldName}({Property.TypeName} {Property.FieldName.ToLowerFirstChar()})
    {{
        {GetPrivateBuilderFieldName()} = {Property.FieldName.ToLowerFirstChar()};
        return this;
    }}
"), generatedPrivateFields);
    }

    protected virtual StringBuilder GenerateDefaultConstructorDetail()
    {
        return new StringBuilder().Append($@"
            {Property.FieldName} = new {Property.TypeName}();");
    }

    protected string GetPrivateBuilderFieldName()
    {
        return $"_{Property.FieldName.ToLowerFirstChar()}";
    }
}
