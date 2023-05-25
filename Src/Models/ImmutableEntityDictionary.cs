using System.Reflection;

namespace Grenat.Functional.DDD.Generators.Models;

public sealed class ImmutableEntityDictionary : IContainerizedDddProperty, IEquatable<ImmutableEntityDictionary>
{
    public INonContainerizedDddProperty InnerDddProperty { get; }
    public string FieldName { get; }
    public string Type { get; }
    public string KeyType { get; }
    public bool DontGenerateSetters { get; }

    public ImmutableEntityDictionary(INonContainerizedDddProperty innerDddProperty,
        string fieldName,
        string keyType,
        string type,
        bool dontGenerateSetters)
    {
        InnerDddProperty = innerDddProperty;
        FieldName = fieldName;
        KeyType = keyType;
        Type = type;
        DontGenerateSetters = dontGenerateSetters;
    }

    public StringBuilder GenerateSetters(string recordName, string varName)
    {
        var propertyName = FieldName.ToLowerFirstChar();

        return new StringBuilder().Append($@"
        public static {recordName} Set{FieldName}(this {recordName} {varName}, ImmutableDictionary<{KeyType}, {InnerDddProperty.Type}> {propertyName})
        {{
            return {varName} with {{ {FieldName} = {propertyName} }};
        }}

        public static Entity<{recordName}> Set{FieldName}(this {recordName} {varName}, ImmutableDictionary<{KeyType}, Entity<{InnerDddProperty.Type}>> {propertyName})
        {{
            return {varName}.SetEntityDictionary({propertyName}, static ({varName}, {propertyName}) => {varName} with {{ {FieldName} = {propertyName} }});
        }}

        public static Entity<{recordName}> Set{FieldName}(this Entity<{recordName}> {varName}, ImmutableDictionary<{KeyType}, Entity<{InnerDddProperty.Type}>> {propertyName})
        {{
            return {varName}.SetEntityDictionary({propertyName}, static ({varName}, {propertyName}) => {varName} with {{ {FieldName} = {propertyName} }});
        }}
");
    }

    public (StringBuilder, ImmutableList<string>) GenerateBuilderDetails(string recordName)
    {
        var generatedPrivateFields = ImmutableList<string>.Empty;

        generatedPrivateFields = generatedPrivateFields.Add(this.GetPrivateBuilderFieldName());

        return (new StringBuilder().Append($@"
        private ImmutableDictionary<{KeyType}, Entity<{InnerDddProperty.Type}>> {this.GetPrivateBuilderFieldName()} {{ get; set; }}
        public {recordName} With{FieldName}(ImmutableDictionary<{KeyType}, Entity<{InnerDddProperty.Type}>> {FieldName.ToLowerFirstChar()})
        {{
            {this.GetPrivateBuilderFieldName()} = {FieldName.ToLowerFirstChar()};
            return this;
        }}
"), generatedPrivateFields);
    }

    public StringBuilder GenerateDefaultConstructorDetail()
    {
        return new StringBuilder().Append($@"
            {FieldName} = ImmutableDictionary<{KeyType}, {InnerDddProperty.Type}>.Empty;");
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as ImmutableEntityDictionary);
    }

    public bool Equals(ImmutableEntityDictionary other)
    {
        return other is not null &&
               EqualityComparer<INonContainerizedDddProperty>.Default.Equals(InnerDddProperty, other.InnerDddProperty) &&
               FieldName == other.FieldName &&
               Type == other.Type &&
               KeyType == other.KeyType &&
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
        int hashCode = -1565450388;
        hashCode = hashCode * -1521134295 + EqualityComparer<INonContainerizedDddProperty>.Default.GetHashCode(InnerDddProperty);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FieldName);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(KeyType);
        hashCode = hashCode * -1521134295 + DontGenerateSetters.GetHashCode();
        return hashCode;
    }

    public static bool operator ==(ImmutableEntityDictionary left, ImmutableEntityDictionary right)
    {
        return EqualityComparer<ImmutableEntityDictionary>.Default.Equals(left, right);
    }

    public static bool operator !=(ImmutableEntityDictionary left, ImmutableEntityDictionary right)
    {
        return !(left == right);
    }
}

public static partial class NamedTypeSymbolExtensions
{
    public static ImmutableEntityDictionary GetImmutableEntityDictionary(this ISymbol memberSymbol, GeneratorSyntaxContext context)
    {
        var namedTypeSymbol = memberSymbol.GetNamedTypeSymbol();

        return new ImmutableEntityDictionary(
            namedTypeSymbol.TypeArguments[1].GetNamedTypeSymbol().GetEntity(context),
            memberSymbol.Name,
            namedTypeSymbol.TypeArguments[0].GetNamedTypeSymbol().Name,
            namedTypeSymbol.GetNamedTypeSymbol().Name,
            memberSymbol.NoSetter(context));
    }
}
