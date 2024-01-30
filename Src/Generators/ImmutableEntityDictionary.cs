//using Grenat.Functional.DDD.Generators.Src.Extensions;
//using Grenat.Functional.DDD.Generators.Src.Models;

//namespace Grenat.Functional.DDD.Generators.Models;

//public sealed class ImmutableEntityDictionary : IContainerizedEntityProperty, IEquatable<ImmutableEntityDictionary>
//{
//    public INonContainerizedEntityProperty InnerProperty { get; }
//    public string FieldName { get; }
//    public string Type { get; }
//    public string KeyType { get; }
//    public bool DontGenerateSetters { get; }

//    public ImmutableEntityDictionary(INonContainerizedEntityProperty innerDddProperty,
//        string fieldName,
//        string keyType,
//        string type,
//        bool dontGenerateSetters)
//    {
//        InnerProperty = innerDddProperty;
//        FieldName = fieldName;
//        KeyType = keyType;
//        Type = type;
//        DontGenerateSetters = dontGenerateSetters;
//    }

//    public StringBuilder GenerateSetters(string recordName, string varName)
//    {
//        var propertyName = FieldName.ToLowerFirstChar();

//        return new StringBuilder().Append($@"
//        public static {recordName} Set{FieldName}(this {recordName} {varName}, ImmutableDictionary<{KeyType}, {InnerProperty.Type}> {propertyName})
//        {{
//            return {varName} with {{ {FieldName} = {propertyName} }};
//        }}

//        public static Entity<{recordName}> Set{FieldName}(this {recordName} {varName}, ImmutableDictionary<{KeyType}, Entity<{InnerProperty.Type}>> {propertyName})
//        {{
//            return {varName}.SetImmutableDictionary({propertyName}, static ({varName}, {propertyName}) => {varName} with {{ {FieldName} = {propertyName} }});
//        }}

//        public static Entity<{recordName}> Set{FieldName}(this Entity<{recordName}> {varName}, ImmutableDictionary<{KeyType}, Entity<{InnerProperty.Type}>> {propertyName})
//        {{
//            return {varName}.SetImmutableDictionary({propertyName}, static ({varName}, {propertyName}) => {varName} with {{ {FieldName} = {propertyName} }});
//        }}
//");
//    }

//    public (StringBuilder, ImmutableList<string>) GenerateBuilderDetails(string recordName)
//    {
//        var generatedPrivateFields = ImmutableList<string>.Empty;

//        generatedPrivateFields = generatedPrivateFields.Add(this.GetPrivateBuilderFieldName());

//        return (new StringBuilder().Append($@"
//        private ImmutableDictionary<{KeyType}, Entity<{InnerProperty.Type}>> {this.GetPrivateBuilderFieldName()} {{ get; set; }}
//        public {recordName} With{FieldName}(ImmutableDictionary<{KeyType}, Entity<{InnerProperty.Type}>> {FieldName.ToLowerFirstChar()})
//        {{
//            {this.GetPrivateBuilderFieldName()} = {FieldName.ToLowerFirstChar()};
//            return this;
//        }}
//"), generatedPrivateFields);
//    }

//    public StringBuilder GenerateDefaultConstructorDetail()
//    {
//        return new StringBuilder().Append($@"
//            {FieldName} = ImmutableDictionary<{KeyType}, {InnerProperty.Type}>.Empty;");
//    }

//    #region IEquatable
//    public override bool Equals(object obj)
//    {
//        return Equals(obj as ImmutableEntityDictionary);
//    }

//    public bool Equals(ImmutableEntityDictionary other)
//    {
//        return other is not null &&
//               EqualityComparer<INonContainerizedEntityProperty>.Default.Equals(InnerProperty, other.InnerProperty) &&
//               FieldName == other.FieldName &&
//               Type == other.Type &&
//               KeyType == other.KeyType &&
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
//        int hashCode = -1565450388;
//        hashCode = hashCode * -1521134295 + EqualityComparer<INonContainerizedEntityProperty>.Default.GetHashCode(InnerProperty);
//        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FieldName);
//        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
//        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(KeyType);
//        hashCode = hashCode * -1521134295 + DontGenerateSetters.GetHashCode();
//        return hashCode;
//    }

//    public static bool operator ==(ImmutableEntityDictionary left, ImmutableEntityDictionary right)
//    {
//        return EqualityComparer<ImmutableEntityDictionary>.Default.Equals(left, right);
//    }

//    public static bool operator !=(ImmutableEntityDictionary left, ImmutableEntityDictionary right)
//    {
//        return !(left == right);
//    }
//    #endregion
//}

