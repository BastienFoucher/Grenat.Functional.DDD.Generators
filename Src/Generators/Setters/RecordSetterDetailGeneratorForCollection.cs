using Grenat.Functional.DDD.Generators.Src.Extensions;
using Grenat.Functional.DDD.Generators.Src.Models;

namespace Grenat.Functional.DDD.Generators.Src.Generators.DefaultConstructor;

internal class RecordSetterDetailGeneratorForCollection : SetterDetailGenerator
{
    private SimpleCollectionProperty _collectionProperty;

    internal RecordSetterDetailGeneratorForCollection(
        IProperty property, 
        string parentClassOrRecordName)
    : base(property, parentClassOrRecordName) 
    {
        _collectionProperty = (SimpleCollectionProperty)property;
    }

    public override StringBuilder Generate()
    {
        var setterParameterName = Property.FieldName.ToLowerFirstChar();
        var propertyName = Property.FieldName;
        var parentParamName = ParentSymbolName.ToLowerFirstChar();

       return new StringBuilder().Append($@"
    {Property.Accessibility} static Entity<{ParentSymbolName}> Set{propertyName}(this {ParentSymbolName} {parentParamName}, ICollection<{_collectionProperty.InnerType.TypeNameWithDddContainer}> {setterParameterName})
    {{
        return {parentParamName}.SetCollection({setterParameterName}, static ({parentParamName}, {setterParameterName}) => {parentParamName} with {{ {propertyName} = {setterParameterName} }});
    }}

    {Property.Accessibility} static Entity<{ParentSymbolName}> Set{propertyName}(this Entity<{ParentSymbolName}> {parentParamName}, ICollection<{_collectionProperty.InnerType.TypeNameWithDddContainer}> {setterParameterName})
    {{
        return {parentParamName}.SetCollection({setterParameterName}, static ({parentParamName}, {setterParameterName}) => {parentParamName} with {{ {propertyName} = {setterParameterName} }});
    }}
");
    }

}
