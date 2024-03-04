using Grenat.Functional.DDD.Generators.Src.Extensions;
using Grenat.Functional.DDD.Generators.Src.Models;

namespace Grenat.Functional.DDD.Generators.Src.Generators.DefaultConstructor;

internal class RecordSetterDetailGeneratorForDictionary : SetterDetailGenerator
{
    private DictionaryProperty _immutableDictionaryProperty;

    internal RecordSetterDetailGeneratorForDictionary(
        IProperty property, 
        string parentClassOrRecordName)
    : base(property, parentClassOrRecordName) 
    {
        _immutableDictionaryProperty = (DictionaryProperty)property;
    }

    public override StringBuilder Generate()
    {
        var setterParameterName = Property.FieldName.ToLowerFirstChar();
        var propertyName = Property.FieldName;
        var parentParamName = ParentSymbolName.ToLowerFirstChar();

       return new StringBuilder().Append($@"
    {Property.Accessibility} static Entity<{ParentSymbolName}> Set{propertyName}(this {ParentSymbolName} {parentParamName}, ImmutableDictionary<{_immutableDictionaryProperty.KeyType.TypeName}, {_immutableDictionaryProperty.InnerType.TypeNameWithDddContainer}> {setterParameterName})
    {{
        return {parentParamName}.SetDictionary({setterParameterName}, static ({parentParamName}, {setterParameterName}) => {parentParamName} with {{ {propertyName} = {setterParameterName} }});
    }}

    {Property.Accessibility} static Entity<{ParentSymbolName}> Set{propertyName}(this Entity<{ParentSymbolName}> {parentParamName}, ImmutableDictionary<{_immutableDictionaryProperty.KeyType.TypeName}, {_immutableDictionaryProperty.InnerType.TypeNameWithDddContainer}> {setterParameterName})
    {{
        return {parentParamName}.SetDictionary({setterParameterName}, static ({parentParamName}, {setterParameterName}) => {parentParamName} with {{ {propertyName} = {setterParameterName} }});
    }}
");
    }

}
