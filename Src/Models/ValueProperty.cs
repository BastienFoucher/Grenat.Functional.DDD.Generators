using Grenat.Functional.DDD.Generators.Src.Extensions;
using Grenat.Functional.DDD.Generators.Src.Models;

namespace Grenat.Functional.DDD.Generators.Models;

public sealed class ValueProperty : IProperty, IEquatable<ValueProperty>
{ 
    public ISymbol Symbol { get; private set; }
    public string FieldName => Symbol.Name;
    public string Accessibility => Symbol.GetAccessibility();

    public string TypeName {get; private set;}
    public ITypeSymbol TypeSymbol { get; private set;}

    public ImmutableArray<ITypeSymbol> InnerTypes { get; private set; }

    public bool DontGenerateSetters { get; set; }

    public ValueProperty(ISymbol symbol,
        string typeName,
        ITypeSymbol typeSymbol,
        ImmutableArray<ITypeSymbol> innerTypes,
        bool dontGenerateSetters)
    {
        Symbol = symbol;
        TypeSymbol = typeSymbol;
        InnerTypes = innerTypes;
        TypeName = GetTypeName(typeName, innerTypes);
        DontGenerateSetters = dontGenerateSetters;
    }

    private string GetTypeName(string typeName, ImmutableArray<ITypeSymbol> innerTypes)
    {
        if (innerTypes.Length > 0)
        {
            var innerTypesString = string.Join(", ", innerTypes);
            return new StringBuilder()
                .Append(typeName)
                .Append("<")
                .Append(innerTypesString)
                .Append(">").ToString();
        }
        else 
            return typeName;
    }

    #region IEquatable
    public override bool Equals(object obj)
    {
        return Equals(obj as ValueProperty);
    }

    public bool Equals(ValueProperty other)
    {
        return other is not null &&
               Symbol.Equals(other.Symbol, SymbolEqualityComparer.Default) &&
               TypeName == other.TypeName &&
               InnerTypes == other.InnerTypes &&
               DontGenerateSetters == other.DontGenerateSetters;
    }

    public override int GetHashCode()
    {
        int hashCode = 1977427925;
        hashCode = hashCode * -1521134295 + SymbolEqualityComparer.Default.GetHashCode(Symbol);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TypeName);
        hashCode = hashCode * -1521134295 + EqualityComparer<ImmutableArray<ITypeSymbol>>.Default.GetHashCode(InnerTypes);
        hashCode = hashCode * -1521134295 + DontGenerateSetters.GetHashCode();
        return hashCode;
    }
    #endregion
}
