using Grenat.Functional.DDD.Generators.Src.Models;

namespace Grenat.Functional.DDD.Generators.Src.Generators.Builder;

internal class BuilderDetailGeneratorForOptionProperty : BuilderDetailGenerator
{
    private DddProperty InnerProperty { get; set; }

    public BuilderDetailGeneratorForOptionProperty(
        IProperty property,
        DddProperty innerProperty,
        string parentClassOrRecordName)
        : base(property, parentClassOrRecordName)
    {
        InnerProperty = innerProperty;
    }

    protected override StringBuilder GenerateDefaultConstructorDetail()
    {
        return new StringBuilder().Append($@"
            {Property.FieldName} = None<{InnerProperty.TypeName}>();");
    }
}
