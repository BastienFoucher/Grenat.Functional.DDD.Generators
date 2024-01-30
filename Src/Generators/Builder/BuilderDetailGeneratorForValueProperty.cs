using Grenat.Functional.DDD.Generators.Src.Extensions;

namespace Grenat.Functional.DDD.Generators.Src.Generators.Builder;

internal class BuilderDetailGeneratorForValueProperty : BuilderDetailGenerator
{
    public BuilderDetailGeneratorForValueProperty(
        IProperty property,
        string parentClassOrRecordName)
    : base(property, parentClassOrRecordName) { }

//    public override (StringBuilder, ImmutableList<string>) Generate()
//    {
//        var generatedPrivateFields = ImmutableList<string>.Empty;

//        generatedPrivateFields = generatedPrivateFields.Add(GetPrivateBuilderFieldName());
//        return (new StringBuilder().Append($@"
//        private Entity<{Property.TypeName}> {this.GetPrivateBuilderFieldName()} {{ get; set; }}
//        public 
//        {ParentSymbolName} With{Property.FieldName}({Property.TypeName} {Property.FieldName.ToLowerFirstChar()})
//        {{
            
//        {this.GetPrivateBuilderFieldName()} = {Property.FieldName.ToLowerFirstChar()};
//            return this;
//        }}
//"), generatedPrivateFields);
//    }

    protected override StringBuilder GenerateDefaultConstructorDetail()
    {
        return new StringBuilder().Append($@"
            {Property.FieldName} = default;");
    }
}
