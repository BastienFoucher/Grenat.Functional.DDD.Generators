//using Grenat.Functional.DDD.Generators.Src.Extensions;
//using Grenat.Functional.DDD.Generators.Src.Models;

//namespace Grenat.Functional.DDD.Generators.Models;

//public sealed class Value : EntityProperty, IEquatable<Value>
//{
//    public Value(
//        string fieldName,
//        string type,
//        bool dontGenerateSetters)
//    {
//        FieldName = fieldName;
//        Type = type;
//        DontGenerateSetters = dontGenerateSetters;
//    }

//    public string FieldName { get; }
//    public string Type { get; }
//    public bool HasDefaultConstructor { get; }
//    public bool DontGenerateSetters { get; }

//    public StringBuilder GenerateSetters(string recordName, string varName)
//    {
//        var methodName = $"Set{FieldName}";
//        var varNameOfPropertyToSet = FieldName.ToLowerFirstChar();
//        var typeOfPropertyToSet = Type;

//        return new StringBuilder().Append($@"
//        public static Entity<{recordName}> {methodName}(this Entity<{recordName}> {varName}, {typeOfPropertyToSet} {varNameOfPropertyToSet})
//        {{
//            return {varName}.Set({varNameOfPropertyToSet}, static ({varName}, {varNameOfPropertyToSet}) => {varName} with {{ {FieldName} = {varNameOfPropertyToSet} }});
//        }}
//");
//    }

//    public (StringBuilder, ImmutableList<string>) GenerateBuilderDetails(string recordName)
//    {
//        var stringBuilder = new StringBuilder();
//        var privateBuilderFieldName = this.GetPrivateBuilderFieldName();

//        stringBuilder.Append($@"
//        private {Type} {privateBuilderFieldName} {{ get; set; }}
//        public {recordName} With{FieldName}({Type} {FieldName.ToLowerFirstChar()})
//        {{
//            {this.GetPrivateBuilderFieldName()} = {FieldName.ToLowerFirstChar()};
//            return this;
//        }}
//");
//        return (stringBuilder, ImmutableList<string>.Empty.Add(privateBuilderFieldName));
//    }

//    public StringBuilder GenerateDefaultConstructorDetail()
//    {
//        return new StringBuilder().Append($@"
//            {FieldName} = default;");
//    }

//    #region IEquatable
//    public override bool Equals(object obj)
//    {
//        return Equals(obj as ValueObjectBase);
//    }

//    public bool Equals(Value other)
//    {
//        return other is not null &&
//               FieldName == other.FieldName &&
//               Type == other.Type &&
//               DontGenerateSetters == other.DontGenerateSetters;
//    }

//    public bool Equals(EntityProperty other)
//    {
//        return other is not null &&
//               FieldName == other.FieldName &&
//               Type == other.TypeName;
//    }

//    public override int GetHashCode()
//    {
//        int hashCode = -324197283;
//        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FieldName);
//        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
//        hashCode = hashCode * -1521134295 + HasDefaultConstructor.GetHashCode();
//        hashCode = hashCode * -1521134295 + DontGenerateSetters.GetHashCode();
//        return hashCode;
//    }

//    #endregion
//}