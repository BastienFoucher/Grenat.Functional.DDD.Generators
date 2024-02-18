using Grenat.Functional.DDD.Generators.Src.Extensions;
using Grenat.Functional.DDD.Generators.Src.Models;

namespace Grenat.Functional.DDD.Generators.Src.Generators.DefaultConstructor;

internal class RecordSetterDetailGeneratorForImmutableCollection : SetterDetailGenerator
{
    private ImmutableCollectionProperty _immutableCollectionProperty;

    internal RecordSetterDetailGeneratorForImmutableCollection(
        IProperty property, 
        string parentClassOrRecordName)
    : base(property, parentClassOrRecordName) 
    {
        _immutableCollectionProperty = (ImmutableCollectionProperty)property;
    }

    public override StringBuilder Generate()
    {
        var setterParameterName = Property.FieldName.ToLowerFirstChar();
        var propertyName = Property.FieldName;
        var parentParamName = ParentSymbolName.ToLowerFirstChar();

       return new StringBuilder().Append($@"
    public static {ParentSymbolName} Set{propertyName}(this {ParentSymbolName} {parentParamName}, {Property.TypeName} {setterParameterName})
    {{
        return {parentParamName} with {{ {setterParameterName} = {setterParameterName} }};
    }}

    public static Entity<{ParentSymbolName}> Set{propertyName}(this {ParentSymbolName} {parentParamName}, ICollection<{_immutableCollectionProperty.InnerType.TypeNameWithDddContainer}> {setterParameterName})
    {{
        return {parentParamName}.SetCollection({setterParameterName}, static ({parentParamName}, {setterParameterName}) => {parentParamName} with {{ {propertyName} = {setterParameterName} }});
    }}

    public static Entity<{ParentSymbolName}> Set{propertyName}(this Entity<{ParentSymbolName}> {parentParamName}, ICollection<{_immutableCollectionProperty.InnerType.TypeNameWithDddContainer}> {setterParameterName})
    {{
        return {parentParamName}.SetCollection({setterParameterName}, static ({parentParamName}, {setterParameterName}) => {parentParamName} with {{ {propertyName} = {setterParameterName} }});
    }}
");
    }

}
