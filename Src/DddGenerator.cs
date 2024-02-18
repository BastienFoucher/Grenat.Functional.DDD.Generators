using Grenat.Functional.DDD.Generators.Src.Extensions;
using Grenat.Functional.DDD.Generators.Src.Generators;

namespace Grenat.Functional.DDD.Generators;

[Generator]
public class DddGenerator : IIncrementalGenerator
{
    private readonly Dictionary<string, int> _extractionCallsPerFileName = new();

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var entities = context.SyntaxProvider.CreateSyntaxProvider(
            predicate: (node, _) => (node is RecordDeclarationSyntax || node is ClassDeclarationSyntax),

            // Make sure the resulting structure implements IEquatable<T> to use the cache of the
            // incremental generator for better performance. Else, the code to extract the structure will be 
            // executed each time a key is typed.
            transform: (ctx, _) => ExtractEntityStructure(ctx)
        ).Where(t => t != null);

        context.RegisterPostInitializationOutput(static ctx => CreateAttributes(ctx));
        context.RegisterSourceOutput(entities, (ctx, source) => GenerateSourceFiles(ctx, source, _extractionCallsPerFileName));
    }

    private static EntityStructure ExtractEntityStructure(GeneratorSyntaxContext context)
    {
        if (context.SemanticModel.GetDeclaredSymbol(context.Node).IsEntity(context))
            return new EntityStructure(context);
        else
            return null;
    }

    private static void GenerateSourceFiles(
        SourceProductionContext context,
        EntityStructure entityStructure,
        Dictionary<string, int> _extractionCallsPerFileName)
    {
        if (!entityStructure.GenerateSetters && !entityStructure.GenerateBuilder && !entityStructure.GenerateDefaultConstructor)
            return;

        var fileName = $"{entityStructure.NameSpaceName}.{entityStructure.Name}.g.cs";

        if (_extractionCallsPerFileName.ContainsKey(fileName))
            _extractionCallsPerFileName[fileName]++;
        else
            _extractionCallsPerFileName.Add(fileName, 1);

        var result = new StringBuilder();
        result = result.Append($"//generation count: {_extractionCallsPerFileName[fileName]}");  // Use it to ensure no excessive generations are called(cf comment on top of this file)
        result.Append($@"
namespace {entityStructure.NameSpaceName};");

        if (entityStructure.GenerateDefaultConstructor)
            result.Append(GenerateDefaultConstructor(entityStructure));

        if (entityStructure.GenerateSetters)
            result.Append(GenerateSetters(entityStructure));

        if (entityStructure.GenerateBuilder)
            result.Append(GenerateBuilder(entityStructure));

        context.AddSource(fileName, result.ToString());
    }

    private static StringBuilder GenerateSetters(EntityStructure entityStructure)
    {
        var generator = new SetterGenerator(entityStructure);
        return generator.Generate();
    }

    public static StringBuilder GenerateBuilder(EntityStructure entityStructure)
    {
        var generator = new BuilderGenerator(entityStructure);
        return generator.Generate();
    }

    public static StringBuilder GenerateDefaultConstructor(EntityStructure entityStructure)
    {
        var generator = new DefaultConstructorGenerator(entityStructure);
        return generator.Generate();
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
}

