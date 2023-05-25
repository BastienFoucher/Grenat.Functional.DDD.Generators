namespace Grenat.Functional.DDD.Generators.Models;

public sealed class ImmutableValueObjectList : IContainerizedDddProperty, IEquatable<ImmutableValueObjectList>
{
    public ImmutableValueObjectList(INonContainerizedDddProperty innerDddProperty, string fieldName, string type, bool dontGenerateSetters)
    {
        InnerDddProperty = innerDddProperty;
        FieldName = fieldName;
        Type = type;
        DontGenerateSetters = dontGenerateSetters;
    }

    public INonContainerizedDddProperty InnerDddProperty { get; }
    public string FieldName { get; }
    public string Type { get; }
    public bool DontGenerateSetters { get; }

    public StringBuilder GenerateSetters(string recordName, string varName)
    {
        var propertyName = FieldName.ToLowerFirstChar();

        return new StringBuilder().Append($@"
        public static {recordName} Set{FieldName}(this {recordName} {varName}, ImmutableList<{InnerDddProperty.Type}> {propertyName})
        {{
            return {varName} with {{ {FieldName} = {propertyName} }};
        }}

        public static Entity<{recordName}> Set{FieldName}(this {recordName} {varName}, ImmutableList<ValueObject<{InnerDddProperty.Type}>> {propertyName})
        {{
            return {varName}.SetValueObjectList({propertyName}, static ({varName}, {propertyName}) => {varName} with {{ {FieldName} = {propertyName} }});
        }}

        public static Entity<{recordName}> Set{FieldName}(this Entity<{recordName}> {varName}, ImmutableList<ValueObject<{InnerDddProperty.Type}>> {propertyName})
        {{
            return {varName}.SetValueObjectList({propertyName}, static ({varName}, {propertyName}) => {varName} with {{ {FieldName} = {propertyName} }});
        }}
");
    }

    public (StringBuilder, ImmutableList<string>) GenerateBuilderDetails(string recordName)
    {
        var generatedPrivateFields = ImmutableList<string>.Empty;

        generatedPrivateFields = generatedPrivateFields.Add(this.GetPrivateBuilderFieldName());

        return (new StringBuilder().Append($@"
        private ImmutableList<ValueObject<{InnerDddProperty.Type}>> {this.GetPrivateBuilderFieldName()} {{ get; set; }}
        public {recordName} With{FieldName}(ImmutableList<ValueObject<{InnerDddProperty.Type}>> {FieldName.ToLowerFirstChar()})
        {{
            {this.GetPrivateBuilderFieldName()} = {FieldName.ToLowerFirstChar()};
            return this;
        }}
"), generatedPrivateFields);
    }

    public StringBuilder GenerateDefaultConstructorDetail()
    {
        return new StringBuilder().Append($@"
            {FieldName} = ImmutableList<{InnerDddProperty.Type}>.Empty;");
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as ImmutableValueObjectList);
    }

    public bool Equals(ImmutableValueObjectList other)
    {
        return other is not null &&
               EqualityComparer<INonContainerizedDddProperty>.Default.Equals(InnerDddProperty, other.InnerDddProperty) &&
               FieldName == other.FieldName &&
               Type == other.Type &&
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
        int hashCode = -1522267040;
        hashCode = hashCode * -1521134295 + EqualityComparer<INonContainerizedDddProperty>.Default.GetHashCode(InnerDddProperty);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FieldName);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
        hashCode = hashCode * -1521134295 + DontGenerateSetters.GetHashCode();
        return hashCode;
    }

    public static bool operator ==(ImmutableValueObjectList left, ImmutableValueObjectList right)
    {
        return EqualityComparer<ImmutableValueObjectList>.Default.Equals(left, right);
    }

    public static bool operator !=(ImmutableValueObjectList left, ImmutableValueObjectList right)
    {
        return !(left == right);
    }
}

public static partial class NamedTypeSymbolExtensions
{
    public static ImmutableValueObjectList GetImmutableValueObjectList(this ISymbol memberSymbol, GeneratorSyntaxContext context)
    {
        var namedTypeSymbol = memberSymbol.GetNamedTypeSymbol();

        return new ImmutableValueObjectList(
            namedTypeSymbol.TypeArguments[0].GetNamedTypeSymbol().GetValueObject(context, memberSymbol.Name),
            memberSymbol.Name,
            namedTypeSymbol.GetNamedTypeSymbol().Name,
            memberSymbol.NoSetter(context));
    }
}
