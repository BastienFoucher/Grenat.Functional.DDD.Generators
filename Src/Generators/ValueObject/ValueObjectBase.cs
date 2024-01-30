//using Grenat.Functional.DDD.Generators.Src.Extensions;

//namespace Grenat.Functional.DDD.Generators.Models;

//public abstract class ValueObjectBase : INonContainerizedEntityProperty
//{
//    protected ValueObjectBase(string fieldName,
//        string type,
//        IEnumerable<ValueObjectProperty> innerValueProperties,
//        bool hasDefaultConstructor,
//        bool dontGenerateSetters)
//    {
//        FieldName = fieldName;
//        Type = type;
//        InnerValueProperties = innerValueProperties;
//        HasDefaultConstructor = hasDefaultConstructor;
//        DontGenerateSetters = dontGenerateSetters;
//    }

//    public string FieldName { get; }
//    public string Type { get; }
//    public IEnumerable<ValueObjectProperty> InnerValueProperties { get; }
//    public bool HasDefaultConstructor { get; }
//    public bool DontGenerateSetters { get; }

//    public abstract StringBuilder GenerateSettersHeader(string recordOrClassName);

//    public abstract StringBuilder GenerateSetters(string recordOrClassName, string varName);

//    public (StringBuilder, ImmutableList<string>) GenerateBuilderDetails(string recordName)
//    {
//        var result = new StringBuilder();
//        var setterParametersList = new StringBuilder();
//        var setterArgumentsList = new StringBuilder();
//        var generatedPrivateFields = ImmutableList<string>.Empty;

//        foreach (var valueObjectProperty in InnerValueProperties)
//        {
//            var paramName = $"{valueObjectProperty.GetPrivateBuilderFieldName().Remove(0, 1)}";

//            result.Append($@"
//        private {valueObjectProperty.TypeName} {valueObjectProperty.GetPrivateBuilderFieldName()} {{ get; set; }}");
//            setterParametersList.Append($"{valueObjectProperty.TypeName} {paramName}, ");
//            setterArgumentsList.Append($@"
//            {valueObjectProperty.GetPrivateBuilderFieldName()} = {paramName};");
//            generatedPrivateFields = generatedPrivateFields.Add(valueObjectProperty.GetPrivateBuilderFieldName());
//        }

//        generatedPrivateFields = generatedPrivateFields.Add(this.GetPrivateBuilderFieldName());

//        result.Append($@"
//        public {recordName} With{FieldName}({setterParametersList.ToString().RemoveLastChars(2)})
//        {{{setterArgumentsList}
//            return this;
//        }}

//        private ValueObject<{Type}> {this.GetPrivateBuilderFieldName()} {{ get; set; }}
//        public {recordName} With{FieldName}(ValueObject<{Type}> {FieldName.ToLowerFirstChar()})
//        {{
//            {this.GetPrivateBuilderFieldName()} = {FieldName.ToLowerFirstChar()};
//            return this;
//        }}
//");

//        return (result, generatedPrivateFields);
//    }

//    public StringBuilder GenerateDefaultConstructorDetail()
//    {
//        return new StringBuilder().Append($@"
//            {FieldName} = new {Type}();");
//    }
//}
