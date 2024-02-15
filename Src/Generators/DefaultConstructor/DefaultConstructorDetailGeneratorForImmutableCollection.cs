using Grenat.Functional.DDD.Generators.Src.Models;

namespace Grenat.Functional.DDD.Generators.Src.Generators.DefaultConstructor;

internal class DefaultConstructorDetailGeneratorForImmutableCollection : DefaultConstructorDetailGenerator
{
    private ImmutableCollectionProperty immutableCollectionProperty;
    internal DefaultConstructorDetailGeneratorForImmutableCollection(
        IProperty property,
        string parentClassOrRecordName) : base(property, parentClassOrRecordName)
    {
        if (!(property is ImmutableCollectionProperty))
            throw new ArgumentException($"{property.FieldName} is not an ImmutableCollection property.");

        immutableCollectionProperty = (ImmutableCollectionProperty)property;
    }

    public override StringBuilder Generate()
    {
        return new StringBuilder().Append($@"
        {Property.FieldName} = {immutableCollectionProperty.TypeName}.Empty;");
    }
}
