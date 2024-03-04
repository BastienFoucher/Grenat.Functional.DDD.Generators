using Grenat.Functional.DDD.Generators.Src.Extensions;
using Grenat.Functional.DDD.Generators.Src.Models;
using System.ComponentModel;

namespace Grenat.Functional.DDD.Generators.Models;

public enum EntitySymbolKind
{
    [Description("class")]
    Class,
    [Description("record")]
    Record
}

public class EntityStructure
{
    public EntitySymbolKind Kind { get; private set; }
    public string NameSpaceName { get; private set; }
    public string Name { get; private set; }
    public IEnumerable<IProperty> Properties { get; private set; }
    public StaticConstructor StaticConstructor { get; private set; }
    public bool GenerateSetters { get; private set; }
    public bool GenerateBuilder { get; private set; }
    public bool GenerateDefaultConstructor { get; private set; }
    public bool HasDefaultContructor { get; private set; }
    public bool HasStaticConstructor { get; private set; }

    public EntityStructure(GeneratorSyntaxContext context)
    {
        var entityDeclarationSyntax = (BaseTypeDeclarationSyntax)context.Node;
        var entitySymbol = context.SemanticModel.GetDeclaredSymbol(entityDeclarationSyntax);

        if (context.Node is RecordDeclarationSyntax)
            Kind = EntitySymbolKind.Record;
        else if (context.Node is ClassDeclarationSyntax)
            Kind = EntitySymbolKind.Class;
        else
            throw new ArgumentException($"{context.Node} is not a supported declaration syntax");

        NameSpaceName = entitySymbol.ContainingNamespace.ToDisplayString();
        Name = entitySymbol.Name;
        Properties = entitySymbol.GetProperties(context);
        StaticConstructor = entitySymbol.GetStaticEntityConstructor(context);
        GenerateSetters = entitySymbol.GenerateSetters(context);
        GenerateBuilder = entitySymbol.GenerateBuilder(context);
        GenerateDefaultConstructor = entitySymbol.GenerateDefaultConstructor(context);
        HasDefaultContructor = entitySymbol.HasDefaultConstructor();
        HasStaticConstructor = StaticConstructor.Any;
    }
}
