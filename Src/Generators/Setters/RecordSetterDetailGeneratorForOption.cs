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
    public static {ParentSymbolName} Set{propertyName}(this {ParentSymbolName} {parentParamName}, {Property.TypeName} {setterParameterName})
    {{
        return {setterParameterName} with {{ {propertyName} = {setterParameterName} }};
    }}

    public static Entity<{ParentSymbolName}> Set{propertyName}(this {ParentSymbolName} {parentParamName}, Option<{_optionProperty.InnerType.TypeNameWithDddContainer}> {setterParameterName})
    {{
        return {setterParameterName}.SetOption({propertyName}, static ({parentParamName}, {setterParameterName}) => {parentParamName} with {{ {propertyName} = {setterParameterName} }});
    }}

    public static Entity<{ParentSymbolName}> Set{propertyName}(this Entity<{ParentSymbolName}> {parentParamName}, Option<{_optionProperty.InnerType.TypeNameWithDddContainer}> {setterParameterName})
    {{
        return {setterParameterName}.SetOption({propertyName}, static ({parentParamName}, {setterParameterName}) => {parentParamName} with {{ {propertyName} = {setterParameterName} }});
    }}
");
    }

}
