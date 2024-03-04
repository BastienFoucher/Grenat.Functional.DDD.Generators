using Grenat.Functional.DDD.Generators.Src.Extensions;
using Grenat.Functional.DDD.Generators.Src.Models;

namespace Grenat.Functional.DDD.Generators.Src.Generators.DefaultConstructor;

internal class RecordSetterDetailGeneratorForOption : SetterDetailGenerator
{
    private OptionProperty _optionProperty;

    internal RecordSetterDetailGeneratorForOption(
        IProperty property, 
        string parentClassOrRecordName)
    : base(property, parentClassOrRecordName) 
    {
        _optionProperty = (OptionProperty)property;
    }

    public override StringBuilder Generate()
    {
        var setterParameterName = Property.FieldName.ToLowerFirstChar();
        var propertyName = Property.FieldName;
        var parentParamName = ParentSymbolName.ToLowerFirstChar();

       return new StringBuilder().Append($@"
    {Property.Accessibility} static Entity<{ParentSymbolName}> Set{propertyName}(this {ParentSymbolName} {parentParamName}, Option<{_optionProperty.InnerType.TypeNameWithDddContainer}> {setterParameterName})
    {{
        return {parentParamName}.SetOption({propertyName}, static ({parentParamName}, {setterParameterName}) => {parentParamName} with {{ {propertyName} = {setterParameterName} }});
    }}

    {Property.Accessibility} static Entity<{ParentSymbolName}> Set{propertyName}(this Entity<{ParentSymbolName}> {parentParamName}, Option<{_optionProperty.InnerType.TypeNameWithDddContainer}> {setterParameterName})
    {{
        return {parentParamName}.SetOption({propertyName}, static ({parentParamName}, {setterParameterName}) => {parentParamName} with {{ {propertyName} = {setterParameterName} }});
    }}
");
    }

}
