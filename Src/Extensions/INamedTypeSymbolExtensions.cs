using Grenat.Functional.DDD.Generators.Src.Models;

namespace Grenat.Functional.DDD.Generators.Src.Extensions;

public static class INamedTypeSymbolExtensions
{
    public static StaticConstructor GetStaticEntityConstructor(
    this INamedTypeSymbol entitySymbol,
    GeneratorSyntaxContext context)
    {
        var staticEntityConstructor = new StaticConstructor(false, string.Empty, string.Empty, ImmutableList<StaticEntityConstructorParameter>.Empty, string.Empty);

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

                var returnType = $"Entity<{returnNamedType.TypeArguments[0].GetNamedTypeSymbol().GetEntityProperty(context).TypeName}>";

                staticEntityConstructor = new StaticConstructor(true,
                    member.ContainingSymbol.Name,
                    member.Name,
                    staticEntityConstructorParameters,
                    returnType);
            }
        }

        return staticEntityConstructor;
    }

    public static ImmutableList<IProperty> GetProperties(
        this INamedTypeSymbol entitySymbol,
        GeneratorSyntaxContext context)
    {
        var properties = ImmutableList<IProperty>.Empty;

        foreach (var member in entitySymbol.GetMembers()
            .Where(ms => ms.IsAPublicFieldOrProperty() && ms.Name != "EqualityContract"))
        {
            var namedTypeSymbol = member.GetNamedTypeSymbol();

            if (namedTypeSymbol.IsValueObject(context))
                properties = properties.Add(member.GetValueObjectProperty(context));

            else if (namedTypeSymbol.IsEntity(context))
                properties = properties.Add(member.GetEntityProperty(context));

            //else if (namedTypeSymbol.IsContainerizedDddProperty(context))
            //    properties = properties.Add(namedTypeSymbol.GetContainerizedDddProperty(context));

            //else
            //    properties = properties.Add(member.GetValueProperty(context));
        }

        return properties;
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
