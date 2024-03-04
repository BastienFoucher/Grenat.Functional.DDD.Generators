using Grenat.Functional.DDD.Generators.Src.Models;

namespace Grenat.Functional.DDD.Generators.Src.Generators.DefaultConstructor;

internal class DefaultConstructorDetailForDictionaryProperty : DefaultConstructorDetailGenerator
{
    private DictionaryProperty dictionaryProperty;
    internal DefaultConstructorDetailForDictionaryProperty(
        IProperty property,
        string parentClassOrRecordName) : base(property, parentClassOrRecordName)
    {
        if (!(property is DictionaryProperty))
            throw new ArgumentException($"{property.FieldName} is not a supported dictionary type.");

        dictionaryProperty = (DictionaryProperty)property;
    }

    public override StringBuilder Generate()
    {
        return dictionaryProperty.IsImmutable
            ? new StringBuilder().Append($@"
        {Property.FieldName} = {dictionaryProperty.TypeName}.Empty;")
            : new StringBuilder().Append($@"
        {Property.FieldName} = new();");
    }
}
