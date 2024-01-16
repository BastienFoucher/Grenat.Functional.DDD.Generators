namespace Grenat.Functional.DDD.Generators.Src.Extensions;

public static class NamedTypeSymbolExtensions
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

    public static ImmutableEntityList GetImmutableEntityList(this ISymbol memberSymbol, GeneratorSyntaxContext context)
    {
        var namedTypeSymbol = memberSymbol.GetNamedTypeSymbol();

        return new ImmutableEntityList(namedTypeSymbol.TypeArguments[0].GetNamedTypeSymbol().GetEntity(context),
            memberSymbol.Name,
            namedTypeSymbol.GetNamedTypeSymbol().Name,
            memberSymbol.NoSetter(context));
    }

    public static ImmutableValueObjectList GetImmutableValueObjectList(this ISymbol memberSymbol, GeneratorSyntaxContext context)
    {
        var namedTypeSymbol = memberSymbol.GetNamedTypeSymbol();

        return new ImmutableValueObjectList(
            namedTypeSymbol.TypeArguments[0].GetNamedTypeSymbol().GetValueObject(context, memberSymbol.Name),
            memberSymbol.Name,
            namedTypeSymbol.GetNamedTypeSymbol().Name,
            memberSymbol.NoSetter(context));
    }

    public static OptionableEntity GetOptionableEntity(this ISymbol memberSymbol, GeneratorSyntaxContext context)
    {
        var namedTypeSymbol = memberSymbol.GetNamedTypeSymbol();

        return new OptionableEntity(
            namedTypeSymbol.TypeArguments[0].GetEntity(context),
            memberSymbol.Name,
            namedTypeSymbol.GetNamedTypeSymbol().Name,
            memberSymbol.NoSetter(context));
    }

    public static OptionableValueObject GetOptionableValueObject(this ISymbol memberSymbol, GeneratorSyntaxContext context)
    {
        var namedTypeSymbol = memberSymbol.GetNamedTypeSymbol();

        return new OptionableValueObject(
            namedTypeSymbol.TypeArguments[0].GetValueObject(context, memberSymbol.Name),
            memberSymbol.Name,
            namedTypeSymbol.GetNamedTypeSymbol().Name,
            memberSymbol.NoSetter(context));
    }

    public static Value GetValue(this ISymbol memberSymbol, GeneratorSyntaxContext context)
    {
        var namedTypeSymbol = memberSymbol.GetNamedTypeSymbol();

        return new Value(
            memberSymbol.Name,
            namedTypeSymbol.GetNamedTypeSymbol().Name,
            memberSymbol.NoSetter(context));
    }

    public static ValueObject GetValueObject(this ISymbol memberSymbol, GeneratorSyntaxContext context, string fieldName)
    {
        var namedTypeSymbol = memberSymbol.GetNamedTypeSymbol();

        if (!namedTypeSymbol.IsValueObject(context))
            throw new ArgumentException($"Named type symbol {namedTypeSymbol.Name} is not a value object.");

        var valueObjectInnerProperties = ImmutableList<ValueObjectMember>.Empty;

        foreach (var valueField in namedTypeSymbol.GetMembers()
            .Where(vo => vo.IsValueField(context)))
        {
            var baseTypeSymbol = valueField.GetNamedTypeSymbol();
            valueObjectInnerProperties = valueObjectInnerProperties.Add(new ValueObjectMember(valueField.Name, baseTypeSymbol.Name, fieldName, false));
        }

        var hasDefaultConstructor = namedTypeSymbol.HasDefaultConstructor();

        return new ValueObject(
            fieldName,
            namedTypeSymbol.GetNamedTypeSymbol().Name,
            valueObjectInnerProperties,
            hasDefaultConstructor,
            memberSymbol.NoSetter(context));
    }
}
