﻿using Grenat.Functional.DDD.Generators.Src.Extensions;
using Grenat.Functional.DDD.Generators.Src.Generators.Builder;
using Grenat.Functional.DDD.Generators.Src.Models;

namespace Grenat.Functional.DDD.Generators.Src.Generators;

internal class BuilderGenerator : IGenerator
{
    private EntityStructure EntityStructure { get; }
    private StaticConstructor StaticConstructor => EntityStructure.StaticConstructor;
    private string BuilderName { get; }

    internal BuilderGenerator(EntityStructure entityStructure)
    {
        EntityStructure = entityStructure;
        BuilderName = $"{EntityStructure.Name}Builder";
    }

    public virtual StringBuilder Generate()
    {
        if (!EntityStructure.HasDefaultContructor) 
            return new StringBuilder(); 

        var result = new StringBuilder().Append($@"
    
public partial {EntityStructure.Kind} {BuilderName}
{{");

        ImmutableList<string> allGeneratedBuilderFields = ImmutableList<string>.Empty;
        foreach (var dddProperty in EntityStructure.Properties)
        {
            var builder = CreateBuilderDetailGenerator(dddProperty);
            (var builderDetails, var generatedFields) = builder.Generate();
            result = result.Append(builderDetails);
            allGeneratedBuilderFields = allGeneratedBuilderFields.AddRange(generatedFields);
        }

        result = result.Append(GenerateBuildMethod(allGeneratedBuilderFields));
        result = result.Append($@"
    }}");
        return result;
    }

    private StringBuilder GenerateBuildMethod(ImmutableList<string> generatedBuilderFields)
    {
        var body = new StringBuilder();
        foreach (var parameter in StaticConstructor.Parameters)
        {
            var matchingPrivateField = generatedBuilderFields.
                Find(f => f.Remove(0, 1).ToLower() == parameter.Name.ToLower());

            // TODO : create a warning if no property could be matched to the parameter
            if (matchingPrivateField != null)
                body.Append($"{matchingPrivateField}, ");
        }

        return new StringBuilder().Append($@"
        public {StaticConstructor.ReturningType} Build() => {StaticConstructor.Name}({body.ToString().RemoveLastChars(2)});
");
    }

    private BuilderDetailGenerator CreateBuilderDetailGenerator(IProperty property)
    {
        if (property is EntityProperty)
            return new BuilderDetailGenerator(property, EntityStructure.Name);
        else /*if (property is ValueObjectProperty)*/
            return new BuilderDetailGeneratorForValueObjectProperty(property, EntityStructure.Name);
        //else if (property is ImmutableDictionaryProperty)
        //    return new BuilderDetailGeneratorForImmutableDictionaryProperty(property, SymbolName, Inn)

    }
}
