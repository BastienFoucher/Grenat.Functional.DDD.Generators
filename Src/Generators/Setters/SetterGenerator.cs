using Grenat.Functional.DDD.Generators.Models;
using Grenat.Functional.DDD.Generators.Src.Extensions;
using Grenat.Functional.DDD.Generators.Src.Generators.DefaultConstructor;
using Grenat.Functional.DDD.Generators.Src.Models;
using System.ComponentModel;

namespace Grenat.Functional.DDD.Generators.Src.Generators;

internal class SetterGenerator : IGenerator
{
    private EntityStructure EntityStructure { get; }

    internal SetterGenerator(EntityStructure entityStructure)
    {
        EntityStructure = entityStructure;
    }

    public virtual StringBuilder Generate()
    {
        var result = new StringBuilder();

        result.Append($@"
public static partial {EntityStructure.Kind.GetAttribute<DescriptionAttribute>().Description} {EntityStructure.Name}Setters
{{");

            foreach (var property in EntityStructure
                .Properties
                .Where(d => !d.DontGenerateSetters))

                result.Append(CreateSetterDetailGenerator(property).Generate());

        result.Append($@"
}}");
        return result;
    }

    private SetterDetailGenerator CreateSetterDetailGenerator(IProperty property)
    {
        if (EntityStructure.Kind == EntitySymbolKind.Record)
        {
            if (property is OptionProperty)
                return new RecordSetterDetailGeneratorForOption(property, EntityStructure.Name);
            else if (property is ImmutableCollectionProperty)
                return new RecordSetterDetailGeneratorForImmutableCollection(property, EntityStructure.Name);
        }
        throw new NotImplementedException();
    }
}
