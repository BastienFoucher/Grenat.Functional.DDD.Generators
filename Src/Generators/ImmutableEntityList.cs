//using Grenat.Functional.DDD.Generators.Src.Extensions;
//using Grenat.Functional.DDD.Generators.Src.Models;

//namespace Grenat.Functional.DDD.Generators.Models;

//public sealed class ImmutableEntityList : IContainerizedEntityProperty, IEquatable<ImmutableEntityList>
//{
//    public ImmutableEntityList(INonContainerizedEntityProperty innerDddProperty, string fieldName, string type, bool dontGenerateSetters)
//    {
//        InnerProperty = innerDddProperty;
//        FieldName = fieldName;
//        Type = type;
//        DontGenerateSetters = dontGenerateSetters;
//    }

//    public INonContainerizedEntityProperty InnerProperty { get; }
//    public string FieldName { get; }
//    public string Type { get; }
//    public bool DontGenerateSetters { get; }

//    public StringBuilder GenerateSetters(string recordName, string varName)
//    {
//        var propertyName = FieldName.ToLowerFirstChar();

//        return new StringBuilder().Append($@"
//        public static {recordName} Set{FieldName}(this {recordName} {varName}, ImmutableList<{InnerProperty.Type}> {propertyName})
//        {{
//            return {varName} with {{ {FieldName} = {propertyName} }};
//        }}

//        public static Entity<{recordName}> Set{FieldName}(this {recordName} {varName}, ImmutableList<Entity<{InnerProperty.Type}>> {propertyName})
//        {{
//            return {varName}.SetImmutableList({propertyName}, static ({varName}, {propertyName}) => {varName} with {{ {FieldName} = {propertyName} }});
//        }}

//        public static Entity<{recordName}> Set{FieldName}(this Entity<{recordName}> {varName}, ImmutableList<Entity<{InnerProperty.Type}>> {propertyName})
//        {{
//            return {varName}.SetImmutableList({propertyName}, static ({varName}, {propertyName}) => {varName} with {{ {FieldName} = {propertyName} }});
//        }}
//");
//    }

//    public (StringBuilder, ImmutableList<string>) GenerateBuilderDetails(string recordName)
//    {
//        var generatedPrivateFields = ImmutableList<string>.Empty;

//        generatedPrivateFields = generatedPrivateFields.Add(this.GetPrivateBuilderFieldName());

//        return (new StringBuilder().Append($@"
//        private ImmutableList<Entity<{InnerProperty.Type}>> {this.GetPrivateBuilderFieldName()} {{ get; set; }}
//        public {recordName} With{FieldName}(ImmutableList<Entity<{InnerProperty.Type}>> {FieldName.ToLowerFirstChar()})
//        {{
//            {this.GetPrivateBuilderFieldName()} = {FieldName.ToLowerFirstChar()};
//            return this;
//        }}
//"), generatedPrivateFields);
//    }

//    public StringBuilder GenerateDefaultConstructorDetail()
//    {
//        return new StringBuilder().Append($@"
//            {FieldName} = ImmutableList<{InnerProperty.Type}>.Empty;");
//    }

//    public override bool Equals(object obj)
//    {
//        return Equals(obj as ImmutableEntityList);
//    }

//    public bool Equals(ImmutableEntityList other)
//    {
//        return other is not null &&
//               EqualityComparer<INonContainerizedEntityProperty>.Default.Equals(InnerProperty, other.InnerProperty) &&
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
//        int hashCode = -1522267040;
//        hashCode = hashCode * -1521134295 + EqualityComparer<INonContainerizedEntityProperty>.Default.GetHashCode(InnerProperty);
//        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FieldName);
//        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
//        hashCode = hashCode * -1521134295 + DontGenerateSetters.GetHashCode();
//        return hashCode;
//    }

//    public static bool operator ==(ImmutableEntityList left, ImmutableEntityList right)
//    {
//        return EqualityComparer<ImmutableEntityList>.Default.Equals(left, right);
//    }

//    public static bool operator !=(ImmutableEntityList left, ImmutableEntityList right)
//    {
//        return !(left == right);
//    }
//}
