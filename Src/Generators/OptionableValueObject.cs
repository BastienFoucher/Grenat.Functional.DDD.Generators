//using Grenat.Functional.DDD.Generators.Src.Extensions;
//using Grenat.Functional.DDD.Generators.Src.Models;

//namespace Grenat.Functional.DDD.Generators.Models;

//public sealed class OptionableValueObject : IContainerizedEntityProperty, IEquatable<OptionableValueObject>
//{
//    public OptionableValueObject(INonContainerizedEntityProperty innerValueObject, string fieldName, string type, bool dontGenerateSetters)
//    {
//        InnerProperty = innerValueObject;
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
//        public static {recordName} Set{FieldName}(this {recordName} {varName}, Option<{InnerProperty.Type}> {propertyName})
//        {{
//            return {varName} with {{ {FieldName} = {propertyName} }};
//        }}

//        public static Entity<{recordName}> Set{FieldName}(this {recordName} {varName}, Option<ValueObject<{InnerProperty.Type}>> {propertyName})
//        {{
//            return {varName}.SetValueObjectOption({propertyName}, static ({varName}, {propertyName}) => {varName} with {{ {FieldName} = {propertyName} }});
//        }}

//        public static Entity<{recordName}> Set{FieldName}(this Entity<{recordName}> {varName}, Option<ValueObject<{InnerProperty.Type}>> {propertyName})
//        {{
//            return {varName}.SetValueObjectOption({propertyName}, static ({varName}, {propertyName}) => {varName} with {{ {FieldName} = {propertyName} }});
//        }}
//");
//    }

//    public (StringBuilder, ImmutableList<string>) GenerateBuilderDetails(string recordName)
//    {
//        var stringBuilder = new StringBuilder();
//        var generatedPrivateFields = ImmutableList<string>.Empty;

//        generatedPrivateFields = generatedPrivateFields.Add($"{this.GetPrivateBuilderFieldName()}Option");

//        stringBuilder.Append($@"
//        private Option<ValueObject<{Type}>> {this.GetPrivateBuilderFieldName()}Option {{ get; set; }}
//        public {recordName} With{FieldName}(Option<ValueObject<{Type}>> {FieldName.ToLowerFirstChar()})
//        {{
//            {this.GetPrivateBuilderFieldName()}Option = {FieldName.ToLowerFirstChar()};
//            return this;
//        }}
//");

//        // Returning also same builder details as an entity : it can be the role of the entity's static constructor
//        // to decide a if an Option<Entity<T>> will be equal to "Some" or "None".
//        var innerValueObject = (ValueObjectBase)InnerProperty;
//        (stringBuilder, generatedPrivateFields) = innerValueObject.GenerateBuilderDetails(recordName);

//        return (stringBuilder, generatedPrivateFields);

//    }

//    public StringBuilder GenerateDefaultConstructorDetail()
//    {
//        return new StringBuilder().Append($@"
//            {FieldName} = None<{InnerProperty.Type}>();");
//    }

//    #region IEquatable
//    public override bool Equals(object obj)
//    {
//        return Equals(obj as OptionableValueObject);
//    }

//    public bool Equals(OptionableValueObject other)
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

//    public static bool operator ==(OptionableValueObject left, OptionableValueObject right)
//    {
//        return EqualityComparer<OptionableValueObject>.Default.Equals(left, right);
//    }

//    public static bool operator !=(OptionableValueObject left, OptionableValueObject right)
//    {
//        return !(left == right);
//    }

//    #endregion
//}