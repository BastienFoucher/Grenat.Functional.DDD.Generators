//using Grenat.Functional.DDD.Generators.Src.Extensions;
//using Grenat.Functional.DDD.Generators.Src.Models;

//namespace Grenat.Functional.DDD.Generators.Src.Generators.Builder;

//internal class BuilderDetailGeneratorForOptionProperty : BuilderDetailGenerator
//{
//    private DddProperty InnerProperty { get; set; }
//    private DddProperty DddProperty { get; }

//    public BuilderDetailGeneratorForOptionProperty(
//        IProperty property,
//        DddProperty innerProperty,
//        string parentClassOrRecordName)
//        : base(property, parentClassOrRecordName)
//    {
//        DddProperty = (DddProperty)property;
//        InnerProperty = innerProperty;
//    }

//    public override (StringBuilder, ImmutableList<string>) Generate()
//    {
//        var generatedPrivateFields = ImmutableList<string>.Empty;

//        generatedPrivateFields = generatedPrivateFields.Add(GetPrivateBuilderFieldName());

//        return (new StringBuilder().Append($@"
//        private {DddProperty.ContainerKindName}<{InnerProperty.ContainerKindName}<{Property.TypeName}>> {this.GetPrivateBuilderFieldName()} {{ get; set; }}
//        public {ParentSymbolName} With{Property.FieldName}({DddProperty.ContainerKindName}<{InnerProperty.ContainerKindName}<{Property.TypeName}>> {Property.FieldName.ToLowerFirstChar()})
//        {{
//            {this.GetPrivateBuilderFieldName()} = {Property.FieldName.ToLowerFirstChar()};
//            return this;
//        }}
//"), generatedPrivateFields);
//    }

//    protected override StringBuilder GenerateDefaultConstructorDetail()
//    {
//        return new StringBuilder().Append($@"
//            {Property.FieldName} = None<{InnerProperty.TypeName}>();");
//    }
//}
