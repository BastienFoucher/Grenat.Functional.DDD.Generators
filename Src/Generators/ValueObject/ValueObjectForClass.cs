//namespace Grenat.Functional.DDD.Generators.Models;

//public sealed class ValueObjectForClass : ValueObjectBase, IEquatable<ValueObjectBase>
//{
//    public ValueObjectForClass(
//        string fieldName,
//        string type,
//        IEnumerable<ValueObjectProperty> innerValueProperties,
//        bool hasDefaultConstructor,
//        bool dontGenerateSetters) 
//        : base(fieldName, type, innerValueProperties, hasDefaultConstructor, dontGenerateSetters)
//    { }

//    public override StringBuilder GenerateSetters(string recordOrClassName, string varName)
//    {
//        var methodName = $"Set{FieldName}";
//        var varNameOfPropertyToSet = FieldName.ToLowerFirstChar();
//        var typeOfPropertyToSet = Type;

//        var setterParametersList = new StringBuilder();
//        var setterArgumentsList = new StringBuilder();

//        foreach (var valueObjectProperty in InnerValueProperties)
//        {
//            setterParametersList.Append($"{valueObjectProperty.TypeName} {valueObjectProperty.FieldName.ToLowerFirstChar()}, ");
//            setterArgumentsList.Append($"{valueObjectProperty.FieldName.ToLowerFirstChar()}, ");
//        }

//        return new StringBuilder().Append($@"
//        public static Entity<{recordOrClassName}> {methodName}(this Entity<{recordOrClassName}> {varName}, {setterParametersList.ToString().RemoveLastChars(2)})
//        {{
//            return {varName}.{methodName}({typeOfPropertyToSet}.Create({setterArgumentsList.ToString().RemoveLastChars(2)}));
//        }}

//        public static {recordOrClassName} {methodName}(this {recordOrClassName} {varName}, {typeOfPropertyToSet} {varNameOfPropertyToSet})
//        {{
//            return {varName}.{FieldName} = {varNameOfPropertyToSet};
//        }}

//        public static Entity<{recordOrClassName}> {methodName}(this {recordOrClassName} {varName}, ValueObject<{typeOfPropertyToSet}> {varNameOfPropertyToSet})
//        {{
//            return {varName}.Set({varNameOfPropertyToSet}, static ({varName}, {varNameOfPropertyToSet}) => {varName}.{FieldName} = {varNameOfPropertyToSet});
//        }}

//        public static Entity<{recordOrClassName}> {methodName}(this Entity<{recordOrClassName}> {varName}, ValueObject<{typeOfPropertyToSet}> {varNameOfPropertyToSet})
//        {{
//            return {varName}.Set({varNameOfPropertyToSet}, static ({varName}, {varNameOfPropertyToSet}) => {varName}.{FieldName} = {varNameOfPropertyToSet});
//        }}
//");
//    }

//    #region IEquatable
//    public bool Equals(ValueObjectBase other)
//    {
//        return other is not null &&
//               FieldName == other.FieldName &&
//               Type == other.Type &&
//               EqualityComparer<IEnumerable<ValueObjectProperty>>.Default.Equals(InnerValueProperties, other.InnerValueProperties) &&
//               HasDefaultConstructor == other.HasDefaultConstructor &&
//               DontGenerateSetters == other.DontGenerateSetters;
//    }
//    #endregion
//}
