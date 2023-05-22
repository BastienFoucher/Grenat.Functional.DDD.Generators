namespace Grenat.Functional.DDD.Generators;

public static class StringExtensions
{
    public static string ToLowerFirstChar(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return char.ToLower(input[0]) + input.Substring(1);
    }

    public static string ToUpperFirstChar(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return char.ToUpper(input[0]) + input.Substring(1);
    }

    public static string RemoveLastChars(this string input, int numberOfChars = 1)
    {
        if (numberOfChars > input.Length)
            return string.Empty;

        return input.Remove(input.Length - numberOfChars, numberOfChars);
    }
}

public static class ISymbolExtensions
{
    public const string NAMESPACE_NAME = "Grenat.Functional.DDD.Generators";

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

    private static bool VerifyAttribute(this ISymbol symbol, GeneratorSyntaxContext context, string attributeName)
    {
        // Ensuring namespace of the attribute comes from this generator
        if (context.SemanticModel.Compilation.GetTypeByMetadataName($"{NAMESPACE_NAME}.{attributeName}") == null)
            return false;

        foreach (var symbolAttribute in symbol.GetAttributes())
        {
            if (symbolAttribute.AttributeClass.Name == attributeName)
                return true;
        }

        return false;
    }

    public static bool GenerateSetters(this INamedTypeSymbol symbol, GeneratorSyntaxContext context)
    {
        return symbol.VerifyAttribute(context, "GenerateSettersAttribute");
    }

    public static bool NoSetter(this ISymbol symbol, GeneratorSyntaxContext context)
    {
        return symbol.VerifyAttribute(context, "NoSetterAttribute");
    }

    public static bool GenerateBuilder(this INamedTypeSymbol symbol, GeneratorSyntaxContext context)
    {
        return symbol.VerifyAttribute(context, "GenerateBuilderAttribute");
    }

    public static bool GenerateDefaultConstructor(this INamedTypeSymbol symbol, GeneratorSyntaxContext context)
    {
        return symbol.VerifyAttribute(context, "GenerateDefaultConstructorAttribute");
    }


    public static bool IsValueObject(this INamedTypeSymbol symbol, GeneratorSyntaxContext context)
    {
        return symbol.VerifyAttribute(context, "ValueObjectAttribute");
    }

    public static bool IsValueField(this ISymbol symbol, GeneratorSyntaxContext context)
    {
        return symbol.IsAPublicFieldOrProperty() && symbol.VerifyAttribute(context, "ValueAttribute");
    }

    public static bool IsEntity(this INamedTypeSymbol symbol, GeneratorSyntaxContext context)
    {
        return symbol.VerifyAttribute(context, "EntityAttribute");
    }

    public static bool IsStaticConstructor(this ISymbol symbol, GeneratorSyntaxContext context)
    {
        return symbol.VerifyAttribute(context, "StaticConstructorAttribute");
    }

    public static bool IsContainerizedDddProperty(this INamedTypeSymbol namedTypeSymbol, GeneratorSyntaxContext context)
    {
        return namedTypeSymbol.IsImmutableEntityList(context)
                || namedTypeSymbol.IsImmutableEntityDictionary(context)
                || namedTypeSymbol.IsOptionableEntity(context)
                || namedTypeSymbol.IsImmutableValueObjectList(context)
                || namedTypeSymbol.IsOptionableValueObject(context);
    }

    public static bool IsImmutableEntityList(this INamedTypeSymbol namedTypeSymbol, GeneratorSyntaxContext context)
    {
        return namedTypeSymbol.Name == "ImmutableList" && namedTypeSymbol.TypeArguments[0].GetNamedTypeSymbol().IsEntity(context);
    }

    public static bool IsImmutableValueObjectList(this INamedTypeSymbol namedTypeSymbol, GeneratorSyntaxContext context)
    {
        return namedTypeSymbol.Name == "ImmutableList" && namedTypeSymbol.TypeArguments[0].GetNamedTypeSymbol().IsValueObject(context);
    }

    public static bool IsImmutableEntityDictionary(this INamedTypeSymbol namedTypeSymbol, GeneratorSyntaxContext context)
    {
        return namedTypeSymbol.Name == "ImmutableDictionary" && namedTypeSymbol.TypeArguments[1].GetNamedTypeSymbol().IsEntity(context);
    }

    public static bool IsOptionableEntity(this INamedTypeSymbol namedTypeSymbol, GeneratorSyntaxContext context)
    {
        return namedTypeSymbol.Name == "Option" && namedTypeSymbol.TypeArguments[0].GetNamedTypeSymbol().IsEntity(context);
    }

    public static bool IsOptionableValueObject(this INamedTypeSymbol namedTypeSymbol, GeneratorSyntaxContext context)
    {
        return namedTypeSymbol.Name == "Option" && namedTypeSymbol.TypeArguments[0].GetNamedTypeSymbol().IsValueObject(context);
    }

    public static BaseDddPropertyType GetDDDPropertyType(this INamedTypeSymbol namedTypeSymbol, GeneratorSyntaxContext context)
    {
        if (namedTypeSymbol.IsValueObject(context))
            return BaseDddPropertyType.ValueObject;
        else if (namedTypeSymbol.IsEntity(context))
            return BaseDddPropertyType.Entity;
        else
            return BaseDddPropertyType.Unknown;
    }

    public static bool HasDefaultConstructor(this INamedTypeSymbol namedTypeSymbol)
    {
        // TODO: returns always true. I still can't make the difference between the default constructor and a "hand written one".
        // https://github.com/dotnet/roslyn/issues/24066
        // Also, check if a Syntax Node can help.
        return namedTypeSymbol.GetMembers()
            .Where(m => m.Kind == SymbolKind.Method
                    && !m.GetMethodSymbol().Parameters.Any()
                    && m.GetMethodSymbol().MethodKind == MethodKind.Constructor).Any();
    }
}
