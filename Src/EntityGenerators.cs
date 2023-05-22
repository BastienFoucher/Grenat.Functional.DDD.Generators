namespace Grenat.Functional.DDD.Generators;

[Generator]
public class EntityGenerators : IIncrementalGenerator
{
    private readonly Dictionary<string, int> _extractionCallsPerFileName = new();

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var entities = context.SyntaxProvider.CreateSyntaxProvider(
            predicate: (node, _) => node is RecordDeclarationSyntax,

            // Make sure the resulting structure implements IEquatable<T> to use the cache of the
            // incremental generator for better performance. Else, the code to extract the structure will be 
            // executed each time a key is typed.
            transform: (ctx, _) => ExtractEntityStructure(ctx)
            )
            .Where(t => t != null);

        context.RegisterPostInitializationOutput(static ctx => CreateAttributes(ctx));
        context.RegisterSourceOutput(entities, (ctx, source) => GenerateSourceFiles(ctx, source, _extractionCallsPerFileName));
    }

    private static void CreateAttributes(IncrementalGeneratorPostInitializationContext context)
    {
        context.AddSource("Grenat.Functional.DDD.Generators.GenerateSettersAttribute.g.cs",
            @"namespace Grenat.Functional.DDD.Generators
{
    public class EntityAttribute : System.Attribute {}

    public class ValueObjectAttribute : System.Attribute {}

    public class ValueAttribute : System.Attribute {}

    public class GenerateSettersAttribute : System.Attribute {}

    public class GenerateBuilderAttribute : System.Attribute {}

    public class GenerateDefaultConstructorAttribute : System.Attribute {}

    public class NoSetterAttribute : System.Attribute {}

    public class StaticConstructorAttribute : System.Attribute {}
}");
    }

    private static EntityStructure ExtractEntityStructure(GeneratorSyntaxContext context)
    {
        var entityDeclarationSyntax = (RecordDeclarationSyntax)context.Node;
        var entitySymbol = context.SemanticModel.GetDeclaredSymbol(entityDeclarationSyntax);

        if (!entitySymbol.IsEntity(context))
            return null;

        return new EntityStructure
        {
            NameSpaceName = entitySymbol.ContainingNamespace.ToDisplayString(),
            Name = entitySymbol.Name,
            GenerateSetters = entitySymbol.GenerateSetters(context),
            GenerateBuilder = entitySymbol.GenerateBuilder(context),
            GenerateDefaultConstructor = entitySymbol.GenerateDefaultConstructor(context),
            DddProperties = GetDddProperties(entitySymbol, context),
            StaticConstructor = GetStaticEntityConstructor(entitySymbol, context),
            HasDefaultContructor = entitySymbol.HasDefaultConstructor()
        };
    }

    public static ImmutableList<IDddProperty> GetDddProperties(INamedTypeSymbol entitySymbol, GeneratorSyntaxContext context)
    {
        var dddProperties = ImmutableList<IDddProperty>.Empty;

        foreach (var member in entitySymbol.GetMembers()
            .Where(ms => ms.IsAPublicFieldOrProperty()))
        {
            if (member.GetNamedTypeSymbol().IsValueObject(context))
                dddProperties = dddProperties.Add(member.GetValueObject(context, member.Name));

            else if (member.GetNamedTypeSymbol().IsEntity(context))
                dddProperties = dddProperties.Add(member.GetEntity(context));

            else if (member.GetNamedTypeSymbol().IsContainerizedDddProperty(context))
                dddProperties = dddProperties.Add(GetContainerizedDddProperty(member, context));
        }

        return dddProperties;
    }

    public static IContainerizedDddProperty GetContainerizedDddProperty(ISymbol memberSymbol, GeneratorSyntaxContext context)
    {
        var namedTypeSymbol = memberSymbol.GetNamedTypeSymbol();

        if (!namedTypeSymbol.TypeArguments.Any())
            throw new ArgumentException($"Named type symbol {namedTypeSymbol.Name} is not a container.");

        if (namedTypeSymbol.IsImmutableValueObjectList(context))
            return memberSymbol.GetImmutableValueObjectList(context);

        else if (namedTypeSymbol.IsOptionableValueObject(context))
            return memberSymbol.GetOptionableValueObject(context);

        else if (namedTypeSymbol.IsImmutableEntityList(context))
            return memberSymbol.GetImmutableEntityList(context);

        else if (namedTypeSymbol.IsImmutableEntityDictionary(context))
            return memberSymbol.GetImmutableEntityDictionary(context);

        else if (namedTypeSymbol.IsOptionableEntity(context))
            return memberSymbol.GetOptionableEntity(context);

        else
            throw new ArgumentException($"Name typed symbol {namedTypeSymbol.Name} is an unknown Grenat DDD container");
    }

    public static StaticEntityConstructor GetStaticEntityConstructor(INamedTypeSymbol entitySymbol, GeneratorSyntaxContext context)
    {
        var staticEntityConstructor = new StaticEntityConstructor(false, string.Empty, string.Empty, ImmutableList<StaticEntityConstructorParameter>.Empty, string.Empty);

        foreach (var member in entitySymbol.GetMembers()
            .Where(ms => ms.IsAPublicMethod()
                && ms.IsStaticConstructor(context)))
        {
            var returnNamedType = member.GetMethodSymbol().ReturnType.GetNamedTypeSymbol();

            // TODO : create a warning if return type of the static constructor does not match Entity<recordName>
            if (returnNamedType != null
                && returnNamedType.Name == "Entity"
                && returnNamedType.TypeArguments.Count() == 1
                && returnNamedType.TypeArguments[0].GetNamedTypeSymbol().IsEntity(context))
            {
                var staticEntityConstructorParameters = member.GetMethodSymbol().Parameters
                    .Select(p => new StaticEntityConstructorParameter(p.Name, p.Type.Name))
                    .ToImmutableList();

                var returnType = $"Entity<{returnNamedType.TypeArguments[0].GetNamedTypeSymbol().GetEntity(context).Type}>";

                staticEntityConstructor = new StaticEntityConstructor(true,
                    member.ContainingSymbol.Name,
                    member.Name,
                    staticEntityConstructorParameters,
                    returnType);
            }
        }

        return staticEntityConstructor;
    }

    private static void GenerateSourceFiles(SourceProductionContext context,
        EntityStructure entityStructure,
        Dictionary<string, int> _extractionCallsPerFileName)
    {
        if (!entityStructure.GenerateSetters && !entityStructure.GenerateBuilder && !entityStructure.GenerateDefaultConstructor)
            return;

        var b = entityStructure.Equals(entityStructure);

        var fileName = $"{entityStructure.NameSpaceName}.{entityStructure.Name}.g.cs";

        if (_extractionCallsPerFileName.ContainsKey(fileName))
            _extractionCallsPerFileName[fileName]++;
        else
            _extractionCallsPerFileName.Add(fileName, 1);

        var result = new StringBuilder();
        result = result.Append($"//generation count: {_extractionCallsPerFileName[fileName]}");  // Use it to ensure no excessive generations are called(cf comment on top of this file)
        result.Append($@"
using Grenat.Functional.DDD;
using static Grenat.Functional.DDD.Entity;
using static Grenat.Functional.DDD.Option;
using static Grenat.Functional.DDD.ValueObject;

namespace {entityStructure.NameSpaceName}
{{");

        if (entityStructure.GenerateDefaultConstructor)
            result.Append(GenerateDefaultConstructor(entityStructure));

        if (entityStructure.GenerateSetters)
            result.Append(GenerateSetters(entityStructure));

        if (entityStructure.GenerateBuilder && entityStructure.StaticConstructor.Any)
            result.Append(GenerateBuilder(entityStructure));

        result = result.Append($@"
}}");

        context.AddSource(fileName, result.ToString());
    }

    private static StringBuilder GenerateSetters(EntityStructure entityStructure)
    {
        var varNameForExtendedRecord = entityStructure.Name.ToLowerFirstChar();

        var result = new StringBuilder().Append($@"
    public static partial class {entityStructure.Name}Setters
    {{");

        foreach (var dddProperty in entityStructure.DddProperties.Where(d => !d.DontGenerateSetters))
            result = result.Append(dddProperty.GenerateSetters(entityStructure.Name, varNameForExtendedRecord));

        result = result.Append($@"
    }}");

        return result;
    }

    public static StringBuilder GenerateBuilder(EntityStructure entityStructure)
    {
        var builderName = $"{entityStructure.Name}Builder";

        var result = new StringBuilder().Append($@"
    
    public partial class {builderName}
    {{");

        ImmutableList<string> allGeneratedBuilderFields = ImmutableList<string>.Empty;
        foreach (var dddProperty in entityStructure.DddProperties)
        {
            (var builderDetails, var generatedFields) = dddProperty.GenerateBuilderDetails(builderName);
            result = result.Append(builderDetails);
            allGeneratedBuilderFields = allGeneratedBuilderFields.AddRange(generatedFields);
        }

        result = result.Append(entityStructure.StaticConstructor.GenerateBuildMethod(allGeneratedBuilderFields));
        result = result.Append($@"
    }}");
        return result;
    }

    public static StringBuilder GenerateDefaultConstructor(EntityStructure entityStructure)
    {
        bool everyValueObjectHasADefaultConstructor = entityStructure
            .DddProperties.OfType<ValueObject>()
            .Any(p => p.HasDefaultConstructor) || !entityStructure.DddProperties.OfType<ValueObject>().Any();


        bool everyEntityHasADefaultContructor = entityStructure
            .DddProperties.OfType<Entity>()
            .Any(p => p.HasDefaultConstructor) || !entityStructure.DddProperties.OfType<Entity>().Any();

        var result = new StringBuilder();

        if (everyValueObjectHasADefaultConstructor && everyEntityHasADefaultContructor)
        {
            result.Append($@"
    public partial record {entityStructure.Name}
    {{
        public {entityStructure.Name}() 
        {{");

            foreach (var dddProperty in entityStructure.DddProperties)
                result.Append(dddProperty.GenerateDefaultConstructorDetail());

            result.Append($@"
        }}
    }}
");
        }

        return result;
    }
}

