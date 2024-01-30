//using Grenat.Functional.DDD.Generators.Src.Extensions;
//using Grenat.Functional.DDD.Generators.Src.Models;

//namespace Grenat.Functional.DDD.Generators.Src.Generators.Builder;

//internal class BuilderDetailGeneratorForImmutableDictionaryProperty : BuilderDetailGenerator
//{
//    private string KeyTypeName { get; set; }
//    private ImmutableDictionaryProperty ImmutableDictionaryProperty { get; set; }

//    public BuilderDetailGeneratorForImmutableDictionaryProperty(
//        IProperty property,
//        string parentClassOrRecordName,
//        DddProperty innerProperty,
//        string keyTypeName)
//        : base(property, parentClassOrRecordName)
//    {
//        ImmutableDictionaryProperty = (ImmutableDictionaryProperty)property;
//        KeyTypeName = keyTypeName;
//    }

//    public override (StringBuilder, ImmutableList<string>) Generate()
//    {
//        var generatedPrivateFields = ImmutableList<string>.Empty;

//        generatedPrivateFields = generatedPrivateFields.Add(this.GetPrivateBuilderFieldName());

//         return (new StringBuilder().Append($@"
//        private ImmutableDictionary<{KeyTypeName}, {ImmutableDictionaryProperty.InnerProperty.TypeName}<{InnerProperty.TypeName}>> {this.GetPrivateBuilderFieldName()} {{ get; set; }}
//        public {ParentSymbolName} With{Property.FieldName}(ImmutableDictionary<{KeyTypeName}, {DddProperty.ContainerKindName}<{InnerProperty.TypeName}>> {Property.FieldName.ToLowerFirstChar()})
//        {{
//            {this.GetPrivateBuilderFieldName()} = {Property.FieldName.ToLowerFirstChar()};
//            return this;
//        }}
//"), generatedPrivateFields);
//    }
//}
