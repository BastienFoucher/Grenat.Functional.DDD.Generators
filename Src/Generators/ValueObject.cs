using Grenat.Functional.DDD.Generators.Src.Extensions;

namespace Grenat.Functional.DDD.Generators.Models;

public sealed class ValueObject : INonContainerizedEntityProperty, IEquatable<ValueObject>
{
    public ValueObject(string fieldName,
        string type,
        IEnumerable<ValueObjectMember> innerValueProperties,
        bool hasDefaultConstructor,
        bool dontGenerateSetters)
    {
        FieldName = fieldName;
        Type = type;
        InnerValueProperties = innerValueProperties;
        HasDefaultConstructor = hasDefaultConstructor;
        DontGenerateSetters = dontGenerateSetters;
    }

    public string FieldName { get; }
    public string Type { get; }
    public IEnumerable<ValueObjectMember> InnerValueProperties { get; }
    public bool HasDefaultConstructor { get; }
    public bool DontGenerateSetters { get; }

    public StringBuilder GenerateSetters(string recordName, string varName)
    {
        var methodName = $"Set{FieldName}";
        var varNameOfPropertyToSet = FieldName.ToLowerFirstChar();
        var typeOfPropertyToSet = Type;

        var setterParametersList = new StringBuilder();
        var setterArgumentsList = new StringBuilder();

        foreach (var valueObjectProperty in InnerValueProperties)
        {
            setterParametersList.Append($"{valueObjectProperty.Type} {valueObjectProperty.FieldName.ToLowerFirstChar()}, ");
            setterArgumentsList.Append($"{valueObjectProperty.FieldName.ToLowerFirstChar()}, ");
        }
        return new StringBuilder().Append($@"
        public static Entity<{recordName}> {methodName}(this Entity<{recordName}> {varName}, {setterParametersList.ToString().RemoveLastChars(2)})
        {{
            return {varName}.{methodName}({typeOfPropertyToSet}.Create({setterArgumentsList.ToString().RemoveLastChars(2)}));
        }}

        public static {recordName} {methodName}(this {recordName} {varName}, {typeOfPropertyToSet} {varNameOfPropertyToSet})
        {{
            return {varName} with {{ {FieldName} = {varNameOfPropertyToSet} }};
        }}

        public static Entity<{recordName}> {methodName}(this {recordName} {varName}, ValueObject<{typeOfPropertyToSet}> {varNameOfPropertyToSet})
        {{
            return {varName}.Set({varNameOfPropertyToSet}, static ({varName}, {varNameOfPropertyToSet}) => {varName} with {{ {FieldName} = {varNameOfPropertyToSet} }});
        }}

        public static Entity<{recordName}> {methodName}(this Entity<{recordName}> {varName}, ValueObject<{typeOfPropertyToSet}> {varNameOfPropertyToSet})
        {{
            return {varName}.Set({varNameOfPropertyToSet}, static ({varName}, {varNameOfPropertyToSet}) => {varName} with {{ {FieldName} = {varNameOfPropertyToSet} }});
        }}
");
    }

    public (StringBuilder, ImmutableList<string>) GenerateBuilderDetails(string recordName)
    {
        var result = new StringBuilder();
        var setterParametersList = new StringBuilder();
        var setterArgumentsList = new StringBuilder();
        var generatedPrivateFields = ImmutableList<string>.Empty;

        foreach (var valueObjectProperty in InnerValueProperties)
        {
            var paramName = $"{valueObjectProperty.GetPrivateBuilderFieldName().Remove(0, 1)}";

            result.Append($@"
        private {valueObjectProperty.Type} {valueObjectProperty.GetPrivateBuilderFieldName()} {{ get; set; }}");
            setterParametersList.Append($"{valueObjectProperty.Type} {paramName}, ");
            setterArgumentsList.Append($@"
            {valueObjectProperty.GetPrivateBuilderFieldName()} = {paramName};");
            generatedPrivateFields = generatedPrivateFields.Add(valueObjectProperty.GetPrivateBuilderFieldName());
        }

        generatedPrivateFields = generatedPrivateFields.Add(this.GetPrivateBuilderFieldName());

        result.Append($@"
        public {recordName} With{FieldName}({setterParametersList.ToString().RemoveLastChars(2)})
        {{{setterArgumentsList}
            return this;
        }}

        private ValueObject<{Type}> {this.GetPrivateBuilderFieldName()} {{ get; set; }}
        public {recordName} With{FieldName}(ValueObject<{Type}> {FieldName.ToLowerFirstChar()})
        {{
            {this.GetPrivateBuilderFieldName()} = {FieldName.ToLowerFirstChar()};
            return this;
        }}
");

        return (result, generatedPrivateFields);
    }

    public StringBuilder GenerateDefaultConstructorDetail()
    {
        return new StringBuilder().Append($@"
            {FieldName} = new {Type}();");
    }

    #region IEquatable
    public override bool Equals(object obj)
    {
        return Equals(obj as ValueObject);
    }

    public bool Equals(ValueObject other)
    {
        return other is not null &&
               FieldName == other.FieldName &&
               Type == other.Type &&
               EqualityComparer<IEnumerable<ValueObjectMember>>.Default.Equals(InnerValueProperties, other.InnerValueProperties) &&
               HasDefaultConstructor == other.HasDefaultConstructor &&
               DontGenerateSetters == other.DontGenerateSetters;
    }

    public bool Equals(IEntityProperty other)
    {
        return other is not null &&
               FieldName == other.FieldName &&
               Type == other.Type;
    }

    public override int GetHashCode()
    {
        int hashCode = -324197283;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FieldName);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
        hashCode = hashCode * -1521134295 + EqualityComparer<IEnumerable<ValueObjectMember>>.Default.GetHashCode(InnerValueProperties);
        hashCode = hashCode * -1521134295 + HasDefaultConstructor.GetHashCode();
        hashCode = hashCode * -1521134295 + DontGenerateSetters.GetHashCode();
        return hashCode;
    }
    #endregion
}
