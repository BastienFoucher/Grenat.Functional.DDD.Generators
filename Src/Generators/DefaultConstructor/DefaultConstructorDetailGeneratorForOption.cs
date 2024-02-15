using Grenat.Functional.DDD.Generators.Src.Models;

namespace Grenat.Functional.DDD.Generators.Src.Generators.DefaultConstructor;

internal class DefaultConstructorDetailGeneratorForOption : DefaultConstructorDetailGenerator
{
    private OptionProperty optionProperty;

    internal DefaultConstructorDetailGeneratorForOption(
        IProperty property, 
        string parentClassOrRecordName) : base(property, parentClassOrRecordName)
    {
        if (!(property is OptionProperty))
            throw new ArgumentException($"{property.FieldName} is not an Option property.");

        optionProperty = (OptionProperty)property;
    } 

    public override StringBuilder Generate()
    {
        return new StringBuilder().Append($@"
        {Property.FieldName} = None<{optionProperty.InnerType.TypeName}>();");
    }
}
