//namespace Grenat.Functional.DDD.Generators.Models;

//public sealed class EntityForRecord : EntityBase, IEquatable<EntityForRecord>
//{
//    public EntityForRecord(
//        string fieldName, 
//        string type, 
//        bool dontGenerateSetters, 
//        bool hasDefaultConstructor) 
//        : base(fieldName, type, dontGenerateSetters, hasDefaultConstructor)
//    { }

//    public override StringBuilder GenerateSetters(string recordOrClassName, string varName)
//    {
//        var innerEntityVarName = FieldName.ToLowerFirstChar();

//        return new StringBuilder().Append($@"
//        public static Entity<{recordOrClassName}> Set{FieldName}(this {recordOrClassName} {varName}, Entity<{Type}> {innerEntityVarName})
//        {{
//            return {varName}.Set({innerEntityVarName}, static ({varName}, {innerEntityVarName}) => {varName} with {{ {FieldName} = {innerEntityVarName} }});
//        }}

//        public static Entity<{recordOrClassName}> Set{FieldName}(this Entity<{recordOrClassName}> {varName}, Entity<{Type}> {innerEntityVarName})
//        {{
//            return {varName}.Set({innerEntityVarName}, static ({varName}, {innerEntityVarName}) => {varName} with {{ {FieldName} = {innerEntityVarName} }});
//        }}
//");
//    }

//    public bool Equals(EntityForRecord other)
//    {
//        return other is not null &&
//               FieldName == other.FieldName &&
//               Type == other.Type &&
//               HasDefaultConstructor == other.HasDefaultConstructor &&
//               DontGenerateSetters == other.DontGenerateSetters;
//    }
//}

