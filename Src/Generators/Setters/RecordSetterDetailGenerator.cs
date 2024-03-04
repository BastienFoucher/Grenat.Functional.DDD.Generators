using Grenat.Functional.DDD.Generators.Src.Extensions;
using Grenat.Functional.DDD.Generators.Src.Models;

namespace Grenat.Functional.DDD.Generators.Src.Generators.DefaultConstructor;

internal class RecordSetterDetailGenerator : SetterDetailGenerator
{
    private IProperty _property;

    internal RecordSetterDetailGenerator(
        IProperty property, 
        string parentClassOrRecordName)
    : base(property, parentClassOrRecordName) 
    {
        _property = property;
    }

    public override StringBuilder Generate()
    {
        var setterParameterName = Property.FieldName.ToLowerFirstChar();
        var propertyName = Property.FieldName;
        var parentParamName = ParentSymbolName.ToLowerFirstChar();

        var result = new StringBuilder();

        if (_property is DddProperty dddProperty) 
            result.Append($@"
    {Property.Accessibility} static Entity<{ParentSymbolName}> Set{propertyName}(this {ParentSymbolName} {parentParamName}, {dddProperty.InnerType.TypeName} {setterParameterName})
    {{
        return {parentParamName}.Set({setterParameterName}, static ({parentParamName}, {setterParameterName}) => {parentParamName} with {{ {propertyName} = {setterParameterName} }});
    }}

    {Property.Accessibility} static Entity<{ParentSymbolName}> Set{propertyName}(this Entity<{ParentSymbolName}> {parentParamName}, {dddProperty.InnerType.TypeName} {setterParameterName})
    {{
        return {parentParamName}.Set({setterParameterName}, static ({parentParamName}, {setterParameterName}) => {parentParamName} with {{ {propertyName} = {setterParameterName} }});
    }}");

       result.Append($@"
    {Property.Accessibility} static Entity<{ParentSymbolName}> Set{propertyName}(this {ParentSymbolName} {parentParamName}, {_property.TypeName} {setterParameterName})
    {{
        return {parentParamName}.Set({setterParameterName}, static ({parentParamName}, {setterParameterName}) => {parentParamName} with {{ {propertyName} = {setterParameterName} }});
    }}

    {Property.Accessibility} static Entity<{ParentSymbolName}> Set{propertyName}(this Entity<{ParentSymbolName}> {parentParamName}, {_property.TypeName} {setterParameterName})
    {{
        return {parentParamName}.Set({setterParameterName}, static ({parentParamName}, {setterParameterName}) => {parentParamName} with {{ {propertyName} = {setterParameterName} }});
    }}
");

        return result;
    }

}
