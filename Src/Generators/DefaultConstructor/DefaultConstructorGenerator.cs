using Grenat.Functional.DDD.Generators.Src.Extensions;
using Grenat.Functional.DDD.Generators.Src.Generators.Builder;
using Grenat.Functional.DDD.Generators.Src.Generators.DefaultConstructor;
using Grenat.Functional.DDD.Generators.Src.Models;
using System.ComponentModel;

namespace Grenat.Functional.DDD.Generators.Src.Generators;

internal class DefaultConstructorGenerator : IGenerator
{
    private EntityStructure EntityStructure { get; }
    private StaticConstructor StaticConstructor => EntityStructure.StaticConstructor;
    private string BuilderName { get; }

    internal DefaultConstructorGenerator(EntityStructure entityStructure)
    {
        EntityStructure = entityStructure;
        BuilderName = $"{EntityStructure.Name}Builder";
    }

    public virtual StringBuilder Generate()
    {
        bool everyValueObjectHasADefaultConstructor = EntityStructure
            .Properties.OfType<ValueObjectProperty>()
            .Any(p => p.HasDefaultConstructor) || !EntityStructure.Properties.OfType<ValueObjectProperty>().Any();

        bool everyEntityHasADefaultContructor = EntityStructure
            .Properties.OfType<EntityProperty>()
            .Any(p => p.HasDefaultConstructor) || !EntityStructure.Properties.OfType<EntityProperty>().Any();

        var result = new StringBuilder();

        if (everyValueObjectHasADefaultConstructor && everyEntityHasADefaultContructor)
        {
            result.Append($@"
public partial {EntityStructure.Kind.GetAttribute<DescriptionAttribute>().Description} {EntityStructure.Name}
{{
    public {EntityStructure.Name}() 
    {{");

            foreach (var property in EntityStructure.Properties)
                result.Append(CreateDefaultConstructorDetailGenerator(property).Generate());

            result.Append($@"
    }}
}}
        ");
        }

        return result;
    }

    private DefaultConstructorDetailGenerator CreateDefaultConstructorDetailGenerator(IProperty property)
    {
        if (property is ImmutableCollectionProperty)
            return new DefaultConstructorDetailGeneratorForImmutableCollection(property, EntityStructure.Name);
        else if (property is ImmutableDictionaryProperty)
            return new DefaultConstructorDetailForImmutableDictionaryProperty(property, EntityStructure.Name);
        else if (property is OptionProperty)
            return new DefaultConstructorDetailGeneratorForOption(property, EntityStructure.Name);
        else
            return new DefaultConstructorDetailGenerator(property, EntityStructure.Name);
    }

}
