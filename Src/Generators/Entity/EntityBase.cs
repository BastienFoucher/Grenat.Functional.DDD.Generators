//using Grenat.Functional.DDD.Generators.Src.Extensions;

//namespace Grenat.Functional.DDD.Generators.Models;

//public abstract class EntityBase : INonContainerizedEntityProperty
//{
//    public string FieldName { get; }
//    public string Type { get; }
//    public bool DontGenerateSetters { get; }
//    public bool HasDefaultConstructor { get; }

//    protected EntityBase(
//        string fieldName, 
//        string type, 
//        bool dontGenerateSetters, 
//        bool hasDefaultConstructor)
//    {
//        FieldName = fieldName;
//        Type = type;
//        DontGenerateSetters = dontGenerateSetters;
//        HasDefaultConstructor = hasDefaultConstructor;
//    }

//    public abstract StringBuilder GenerateSetters(string recordOrClassName, string varName);

//    public (StringBuilder, ImmutableList<string>) GenerateBuilderDetails(string recordName)
//    {
//        var generatedPrivateFields = ImmutableList<string>.Empty;

//        generatedPrivateFields = generatedPrivateFields.Add(this.GetPrivateBuilderFieldName());

//        return (new StringBuilder().Append($@"
//        private Entity<{Type}> {this.GetPrivateBuilderFieldName()} {{ get; set; }}
//        public {recordName} With{FieldName}(Entity<{Type}> {FieldName.ToLowerFirstChar()})
//        {{
//            {this.GetPrivateBuilderFieldName()} = {FieldName.ToLowerFirstChar()};
//            return this;
//        }}
//"), generatedPrivateFields);
//    }

//    public StringBuilder GenerateDefaultConstructorDetail()
//    {
//        return new StringBuilder().Append($@"
//            {FieldName} = new {Type}();");
//    }
//}
