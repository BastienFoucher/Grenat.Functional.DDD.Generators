using Grenat.Functional.DDD.Generators.Src.Models;

namespace Grenat.Functional.DDD.Generators.Src.Extensions;

public static class ISymbolExtensions
{
    public const string NAMESPACE_NAME = "Grenat.Functional.DDD.Generators";

    public static EntityProperty GetEntityProperty(
         this ISymbol memberSymbol,
         GeneratorSyntaxContext context)
    {
        var namedTypeSymbol = memberSymbol.GetNamedTypeSymbol();

        if (!namedTypeSymbol.IsEntity(context))
            throw new ArgumentException($"Named type symbol {namedTypeSymbol.Name} is not an entity.");
        return new EntityProperty(
                memberSymbol.Name,
                namedTypeSymbol,
                new TypeData(namedTypeSymbol),
                memberSymbol.GetNamedTypeSymbol().HasDefaultConstructor(),
                memberSymbol.NoSetter(context));
    }

    public static ImmutableDictionaryProperty GetImmutableDictionaryProperty(
        this ISymbol memberSymbol,
        GeneratorSyntaxContext context)
    {
        var namedTypeSymbol = memberSymbol.GetNamedTypeSymbol();

        return new ImmutableDictionaryProperty(
            memberSymbol.Name,
            namedTypeSymbol,
            new TypeData(namedTypeSymbol.TypeArguments[0].GetNamedTypeSymbol()),
            new TypeData(namedTypeSymbol.TypeArguments[1].GetNamedTypeSymbol()),
            memberSymbol.NoSetter(context));
    }

    public static ImmutableCollectionProperty GetImmutableCollectionProperty(
        this ISymbol memberSymbol,
        GeneratorSyntaxContext context)
    {
        var namedTypeSymbol = memberSymbol.GetNamedTypeSymbol();

        return new ImmutableListProperty(
            memberSymbol.Name,
            namedTypeSymbol,
            new TypeData(namedTypeSymbol.TypeArguments[0]),
            memberSymbol.NoSetter(context));
    }

    public static ImmutableCollectionProperty GetImmutableHashSetProperty(
    this ISymbol memberSymbol,
    GeneratorSyntaxContext context)
    {
        var namedTypeSymbol = memberSymbol.GetNamedTypeSymbol();

        return new ImmutableHashSetProperty(
            memberSymbol.Name,
            namedTypeSymbol,
            new TypeData(namedTypeSymbol.TypeArguments[0]),
            memberSymbol.NoSetter(context));
    }


    public static OptionProperty GetOptionableProperty(
        this ISymbol memberSymbol,
        GeneratorSyntaxContext context)
    {
        var namedTypeSymbol = memberSymbol.GetNamedTypeSymbol();

        return new OptionProperty(
            memberSymbol.Name,
            namedTypeSymbol,
            new TypeData(namedTypeSymbol.TypeArguments[0]),
            false,
            memberSymbol.NoSetter(context));
    }

    public static ValueProperty GetValueProperty(
        this ISymbol memberSymbol,
        GeneratorSyntaxContext context)
    {
        var namedTypeSymbol = memberSymbol.GetNamedTypeSymbol();

        return new ValueProperty(
            memberSymbol.Name,
            namedTypeSymbol.GetNamedTypeSymbol().Name,
            namedTypeSymbol,
            namedTypeSymbol.TypeArguments,
            memberSymbol.NoSetter(context));
    }

    public static ValueObjectProperty GetValueObjectProperty(
        this ISymbol memberSymbol,
        GeneratorSyntaxContext context)
    {
        var namedTypeSymbol = memberSymbol.GetNamedTypeSymbol();

        if (!namedTypeSymbol.IsValueObject(context))
            throw new ArgumentException($"Named type symbol {namedTypeSymbol.Name} is not a value object.");

        var valueObjectInnerProperties = ImmutableList<ValueProperty>.Empty;

        foreach (var valueField in namedTypeSymbol.GetMembers()
            .Where(vo => vo.IsValueField(context)))
        {
            var baseTypeSymbol = valueField.GetNamedTypeSymbol();
            valueObjectInnerProperties = valueObjectInnerProperties.Add(
                new ValueProperty(valueField.Name,
                    baseTypeSymbol.Name,
                    namedTypeSymbol,
                    baseTypeSymbol.TypeArguments,
                    false));
        }

        var hasDefaultConstructor = namedTypeSymbol.HasDefaultConstructor();

        return new ValueObjectProperty(
                memberSymbol.Name,
                namedTypeSymbol,
                new TypeData(namedTypeSymbol.GetNamedTypeSymbol()),
                valueObjectInnerProperties,
                hasDefaultConstructor,
                memberSymbol.NoSetter(context));
    }

    public static bool IsAPublicFieldOrProperty(this ISymbol symbol)
    {
        return (symbol.Kind.Equals(SymbolKind.Property)) || (symbol.Kind.Equals(SymbolKind.Field))
                                && symbol.DeclaredAccessibility == Accessibility.Public;
    }

    public static bool IsAPublicMethod(this ISymbol symbol)
    {
        return (symbol.Kind.Equals(SymbolKind.Method)) && symbol.DeclaredAccessibility == Accessibility.Public;
    }

    public static INamedTypeSymbol GetNamedTypeSymbol(this ISymbol symbol)
    {
        ITypeSymbol baseTypeSymbol;

        if (symbol is INamedTypeSymbol result)
            return result;

        else if (symbol.Kind.Equals(SymbolKind.Property))
            baseTypeSymbol = ((IPropertySymbol)symbol).Type;

        else if (symbol.Kind.Equals(SymbolKind.Field))
            baseTypeSymbol = ((IFieldSymbol)symbol).Type;

        else
            throw new ArgumentException($"Cannot get the symbol type of {symbol.Kind}");

        return (INamedTypeSymbol)baseTypeSymbol;
    }

    public static IMethodSymbol GetMethodSymbol(this ISymbol symbol)
    {
        return (IMethodSymbol)symbol;
    }

    private static bool VerifyAttribute(
        this ISymbol symbol,
        string attributeName,
        GeneratorSyntaxContext? context = null
        )
    {
        // Ensuring namespace of the attribute comes from this generator
        if (context != null && ((GeneratorSyntaxContext)context).SemanticModel.Compilation.GetTypeByMetadataName($"{NAMESPACE_NAME}.{attributeName}") == null)
            return false;

        foreach (var symbolAttribute in symbol.GetAttributes())
        {
            if (symbolAttribute.AttributeClass.Name == attributeName)
                return true;
        }

        return false;
    }

    public static bool IsValueField(this ISymbol symbol, GeneratorSyntaxContext? context = null)
    {
        return symbol.IsAPublicFieldOrProperty() && symbol.VerifyAttribute("ValueAttribute", context);
    }

    public static bool IsEntity(this ISymbol symbol, GeneratorSyntaxContext? context = null)
    {
        return symbol.VerifyAttribute("EntityAttribute", context);
    }

    public static bool IsStaticConstructor(this ISymbol symbol, GeneratorSyntaxContext? context = null)
    {
        return symbol.VerifyAttribute("StaticConstructorAttribute", context);
    }

    public static bool GenerateSetters(this ISymbol symbol, GeneratorSyntaxContext? context = null)
    {
        return symbol.VerifyAttribute("GenerateSettersAttribute", context);
    }


    public static bool IsValueObject(this ISymbol symbol, GeneratorSyntaxContext? context = null)
    {
        return symbol.VerifyAttribute("ValueObjectAttribute", context);
    }


    public static bool NoSetter(this ISymbol symbol, GeneratorSyntaxContext? context = null)
    {
        return symbol.VerifyAttribute("NoSetterAttribute", context);
    }

    public static bool GenerateBuilder(this ISymbol symbol, GeneratorSyntaxContext? context = null)
    {
        return symbol.VerifyAttribute("GenerateBuilderAttribute", context);
    }

    public static bool GenerateDefaultConstructor(this ISymbol symbol, GeneratorSyntaxContext? context = null)
    {
        return symbol.VerifyAttribute("GenerateDefaultConstructorAttribute", context);
    }
}
