namespace Grenat.Functional.DDD.Generators.Models;

public sealed class ValueObject : INonContainerizedDddProperty, IEquatable<ValueObject>
{
    public ValueObject(string fieldName,
        string type,
        IEnumerable<ValueObjectProperty> innerValueProperties,
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
    public IEnumerable<ValueObjectProperty> InnerValueProperties { get; }
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

        public static Entity<{recordName}> {methodName}(this Entity<{recordName}> {varName}, ValueObject<{typeOfPropertyToSet}> {varNameOfPropertyToSet})
        {{
            return {varName}.SetValueObject({varNameOfPropertyToSet}, static ({varName}, {varNameOfPropertyToSet}) => {varName} with {{ {FieldName} = {varNameOfPropertyToSet} }});
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

    public override bool Equals(object obj)
    {
        return Equals(obj as ValueObject);
    }

    public bool Equals(ValueObject other)
    {
        return other is not null &&
               FieldName == other.FieldName &&
               Type == other.Type &&
               EqualityComparer<IEnumerable<ValueObjectProperty>>.Default.Equals(InnerValueProperties, other.InnerValueProperties) &&
               HasDefaultConstructor == other.HasDefaultConstructor &&
               DontGenerateSetters == other.DontGenerateSetters;
    }

    public bool Equals(IDddProperty other)
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
        hashCode = hashCode * -1521134295 + EqualityComparer<IEnumerable<ValueObjectProperty>>.Default.GetHashCode(InnerValueProperties);
        hashCode = hashCode * -1521134295 + HasDefaultConstructor.GetHashCode();
        hashCode = hashCode * -1521134295 + DontGenerateSetters.GetHashCode();
        return hashCode;
    }
}

public static partial class NamedTypeSymbolExtensions
{
    public static ValueObject GetValueObject(this ISymbol memberSymbol, GeneratorSyntaxContext context, string fieldName)
    {
        var namedTypeSymbol = memberSymbol.GetNamedTypeSymbol();

        if (!namedTypeSymbol.IsValueObject(context))
            throw new ArgumentException($"Named type symbol {namedTypeSymbol.Name} is not a value object.");

        var valueObjectInnerProperties = ImmutableList<ValueObjectProperty>.Empty;

        foreach (var valueField in namedTypeSymbol.GetMembers()
            .Where(vo => vo.IsValueField(context)))
        {
            var baseTypeSymbol = valueField.GetNamedTypeSymbol();
            valueObjectInnerProperties = valueObjectInnerProperties.Add(new ValueObjectProperty(valueField.Name, baseTypeSymbol.Name, fieldName));
        }

        var hasDefaultConstructor = namedTypeSymbol.HasDefaultConstructor();

        return new ValueObject(
            fieldName,
            namedTypeSymbol.GetNamedTypeSymbol().Name,
            valueObjectInnerProperties,
            hasDefaultConstructor,
            memberSymbol.NoSetter(context));
    }

}
