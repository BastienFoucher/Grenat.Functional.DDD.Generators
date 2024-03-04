using Grenat.Functional.DDD.Generators.Src.Models;

namespace Grenat.Functional.DDD.Generators.Src.Generators.DefaultConstructor;

internal class DefaultConstructorDetailGeneratorForCollection : DefaultConstructorDetailGenerator
{
    private SimpleCollectionProperty collectionProperty;
    internal DefaultConstructorDetailGeneratorForCollection(
        IProperty property,
        string parentClassOrRecordName) : base(property, parentClassOrRecordName)
    {
        if (!(property is SimpleCollectionProperty))
            throw new ArgumentException($"{property.FieldName} is not a supported collection type.");

        collectionProperty = (SimpleCollectionProperty)property;
    }

    public override StringBuilder Generate()
    {
        return collectionProperty.IsImmutable
            ? new StringBuilder().Append($@"
        {Property.FieldName} = {collectionProperty.TypeName}.Empty;")
            : new StringBuilder().Append($@"
        {Property.FieldName} = new();");
    }
}
