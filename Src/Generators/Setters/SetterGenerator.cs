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
public static class {EntityStructure.Name}Setters
{{");

        foreach (var property in EntityStructure
            .Properties
            .Where(d => !d.DontGenerateSetters))

            result.Append(CreateSetterDetailGenerator(property).Generate());

        result.Append($@"
}}");
        return result;
    }

    private SetterDetailGenerator CreateSetterDetailGenerator(IProperty property) =>
        EntityStructure.Kind switch
        {
            EntitySymbolKind.Record => property switch
            {
                OptionProperty => new RecordSetterDetailGeneratorForOption(property, EntityStructure.Name),
                SimpleCollectionProperty => new RecordSetterDetailGeneratorForCollection(property, EntityStructure.Name),
                DictionaryProperty => new RecordSetterDetailGeneratorForDictionary(property, EntityStructure.Name),
                IProperty => new RecordSetterDetailGenerator(property, EntityStructure.Name),
            },
            _ => throw new NotImplementedException()

        };
}
