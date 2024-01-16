using Grenat.Functional.DDD.Generators.Src.Extensions;

namespace Grenat.Functional.DDD.Generators.Models;

public sealed class Entity : INonContainerizedEntityProperty, IEquatable<Entity>
{

    public string FieldName { get; }
    public string Type { get; }
    public bool DontGenerateSetters { get; }
    public bool HasDefaultConstructor { get; }

    public Entity(string fieldName, string type, bool dontGenerateSetters, bool hasDefaultConstructor)
    {
        FieldName = fieldName;
        Type = type;
        DontGenerateSetters = dontGenerateSetters;
        HasDefaultConstructor = hasDefaultConstructor;
    }

    public StringBuilder GenerateSetters(string recordName, string varName)
    {
        var innerEntityVarName = FieldName.ToLowerFirstChar();

        return new StringBuilder().Append($@"
        public static Entity<{recordName}> Set{FieldName}(this {recordName} {varName}, Entity<{Type}> {innerEntityVarName})
        {{
            return {varName}.Set({innerEntityVarName}, static ({varName}, {innerEntityVarName}) => {varName} with {{ {FieldName} = {innerEntityVarName} }});
        }}

        public static Entity<{recordName}> Set{FieldName}(this Entity<{recordName}> {varName}, Entity<{Type}> {innerEntityVarName})
        {{
            return {varName}.Set({innerEntityVarName}, static ({varName}, {innerEntityVarName}) => {varName} with {{ {FieldName} = {innerEntityVarName} }});
        }}
");
    }

    public (StringBuilder, ImmutableList<string>) GenerateBuilderDetails(string recordName)
    {
        var generatedPrivateFields = ImmutableList<string>.Empty;

        generatedPrivateFields = generatedPrivateFields.Add(this.GetPrivateBuilderFieldName());

        return (new StringBuilder().Append($@"
        private Entity<{Type}> {this.GetPrivateBuilderFieldName()} {{ get; set; }}
        public {recordName} With{FieldName}(Entity<{Type}> {FieldName.ToLowerFirstChar()})
        {{
            {this.GetPrivateBuilderFieldName()} = {FieldName.ToLowerFirstChar()};
            return this;
        }}
"), generatedPrivateFields);
    }

    public StringBuilder GenerateDefaultConstructorDetail()
    {
        return new StringBuilder().Append($@"
            {FieldName} = new {Type}();");
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Entity);
    }

    public bool Equals(Entity other)
    {
        return other is not null &&
               FieldName == other.FieldName &&
               Type == other.Type &&
               DontGenerateSetters == other.DontGenerateSetters &&
               HasDefaultConstructor == other.HasDefaultConstructor;
    }


    public bool Equals(IEntityProperty other)
    {
        return other is not null &&
               FieldName == other.FieldName &&
               Type == other.Type;
    }

    public override int GetHashCode()
    {
        int hashCode = -1928875172;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FieldName);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
        hashCode = hashCode * -1521134295 + DontGenerateSetters.GetHashCode();
        hashCode = hashCode * -1521134295 + HasDefaultConstructor.GetHashCode();
        return hashCode;
    }
}

public static partial class NamedTypeSymbolExtensions
{
    public static Entity GetEntity(this ISymbol memberSymbol,
        GeneratorSyntaxContext context)
    {
        var namedTypeSymbol = memberSymbol.GetNamedTypeSymbol();

        if (!namedTypeSymbol.IsEntity(context))
            throw new ArgumentException($"Named type symbol {namedTypeSymbol.Name} is not an entity.");

        return new Entity(
            memberSymbol.Name,
            namedTypeSymbol.GetNamedTypeSymbol().Name,
            memberSymbol.NoSetter(context),
            memberSymbol.GetNamedTypeSymbol().HasDefaultConstructor());
    }
}
