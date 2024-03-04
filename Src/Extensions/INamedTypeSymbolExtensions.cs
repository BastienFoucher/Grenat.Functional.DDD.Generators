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

                var returnType = $"{returnNamedType.TypeArguments[0].GetNamedTypeSymbol().GetEntityProperty(context).TypeName}";

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

        foreach (var member in entitySymbol.GetMembersAndBaseMembers()
            .Where(ms => ms.IsAFieldOrProperty() 
                            && ms.Name != "EqualityContract"))
        {
            var namedTypeSymbol = member.GetNamedTypeSymbol();

            if (namedTypeSymbol.IsValueObject(context))
                properties = properties.Add(member.GetValueObjectProperty(context));

            else if (namedTypeSymbol.IsEntity(context))
                properties = properties.Add(member.GetEntityProperty(context));

            else if (namedTypeSymbol.IsOption(context))
                properties = properties.Add(member.GetOptionableProperty(context));

            else if (namedTypeSymbol.IsCollection(context))
                properties = properties.Add(member.GetCollectionProperty(context));

            else if (namedTypeSymbol.IsDictionary(context))
                properties = properties.Add(member.GetDictionaryProperty(context));

            //else if (namedTypeSymbol.IsContainerizedDddProperty(context))
            //    properties = properties.Add(namedTypeSymbol.GetContainerizedDddProperty(context));

            else
                properties = properties.Add(member.GetValueProperty(context));
        }

        return properties;
    }

    private static ImmutableList<ISymbol> GetMembersAndBaseMembers(
        this INamedTypeSymbol namedTypeSymbol,
        ImmutableList<ISymbol> membersAccumulator = null)
    {
        if (membersAccumulator == null)
            membersAccumulator = ImmutableList<ISymbol>.Empty;

        if (namedTypeSymbol.BaseType != null)
            membersAccumulator = membersAccumulator.AddRange(GetMembersAndBaseMembers(namedTypeSymbol.BaseType, membersAccumulator));
            
        membersAccumulator = membersAccumulator.AddRange(namedTypeSymbol.GetMembers().ToImmutableList());

        return membersAccumulator;
    }


    public static bool IsCollection(this INamedTypeSymbol namedTypeSymbol, GeneratorSyntaxContext context)
    {
        return namedTypeSymbol.Name.Contains("List")
            || namedTypeSymbol.Name.Contains("HashSet")
            || namedTypeSymbol.Name.Contains("Array");

    }

    public static bool IsDictionary(this INamedTypeSymbol namedTypeSymbol, GeneratorSyntaxContext context)
    {
        //return namedTypeSymbol.Name == "ImmutableDictionary" && namedTypeSymbol.TypeArguments[1].GetNamedTypeSymbol().IsEntity(context);
        return namedTypeSymbol.Name.Contains("Dictionary");
    }

    public static bool IsOption(this INamedTypeSymbol namedTypeSymbol, GeneratorSyntaxContext context)
    {
        //return namedTypeSymbol.Name == "Option" && namedTypeSymbol.TypeArguments[0].GetNamedTypeSymbol().IsEntity(context);
        return namedTypeSymbol.Name == "Option";
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

    public static string GetFullTypeName(this INamedTypeSymbol namedTypeSymbol)
    {
        return namedTypeSymbol.Name;
    }

}
