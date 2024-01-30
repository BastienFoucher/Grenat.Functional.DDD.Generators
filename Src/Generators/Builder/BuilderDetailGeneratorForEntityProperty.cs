//using Grenat.Functional.DDD.Generators.Src.Extensions;
//using Grenat.Functional.DDD.Generators.Src.Models;

//namespace Grenat.Functional.DDD.Generators.Src.Generators.Builder;

//internal class BuilderDetailGeneratorForEntityProperty : BuilderDetailGenerator
//{
//    private EntityProperty EntityProperty { get; }

//    public BuilderDetailGeneratorForEntityProperty(
//        IProperty property,
//        string parentClassOrRecordName)
//        : base(property, parentClassOrRecordName)
//    {
//        EntityProperty = (EntityProperty)property;
//    }

//    public override (StringBuilder, ImmutableList<string>) Generate()
//    {
//        var generatedPrivateFields = ImmutableList<string>.Empty;

//        generatedPrivateFields = generatedPrivateFields.Add(GetPrivateBuilderFieldName());

//        return (new StringBuilder().Append($@"
//        private Entity<{Property.TypeName}> {this.GetPrivateBuilderFieldName()} {{ get; set; }}
//        public {ParentSymbolName} With{EntityProperty.FieldName}({EntityProperty.TypeName} {Property.FieldName.ToLowerFirstChar()})
//        {{
//            {this.GetPrivateBuilderFieldName()} = {EntityProperty.FieldName.ToLowerFirstChar()};
//            return this;
//        }}
//"), generatedPrivateFields);
//    }
//}
