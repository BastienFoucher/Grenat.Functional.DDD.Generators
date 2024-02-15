using Grenat.Functional.DDD.Generators.Src.Models;

namespace Grenat.Functional.DDD.Generators.Src.Generators.DefaultConstructor;

internal class DefaultConstructorDetailForImmutableDictionaryProperty : DefaultConstructorDetailGenerator
{
    private ImmutableDictionaryProperty immutableDictionaryProperty;
    internal DefaultConstructorDetailForImmutableDictionaryProperty(
        IProperty property,
        string parentClassOrRecordName) : base(property, parentClassOrRecordName)
    {
        if (!(property is ImmutableDictionaryProperty))
            throw new ArgumentException($"{property.FieldName} is not an ImmutableCollection property.");

        immutableDictionaryProperty = (ImmutableDictionaryProperty)property;
    }

    public override StringBuilder Generate()
    {
        return new StringBuilder().Append($@"
        {Property.FieldName} = {immutableDictionaryProperty.TypeName}.Empty;");
    }
}
