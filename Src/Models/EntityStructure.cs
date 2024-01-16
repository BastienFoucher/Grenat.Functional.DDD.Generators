using System.Linq;
using Grenat.Functional.DDD.Generators.Src.Generators;

namespace Grenat.Functional.DDD.Generators.Models;

public sealed class EntityStructure : IEquatable<EntityStructure>
{
    public string NameSpaceName { get; set; }
    public string Name { get; set; }
    public IEnumerable<IProperty> Properties { get; set; }
    public Builder StaticConstructor { get; set; }
    public bool GenerateSetters { get; set; }
    public bool GenerateBuilder { get; set; }
    public bool GenerateDefaultConstructor { get; set; }
    public bool HasDefaultContructor { get; set; }

    public override bool Equals(object obj)
    {
        return Equals(obj as EntityStructure);
    }

    public bool Equals(EntityStructure other)
    {
        return other is not null &&
               NameSpaceName == other.NameSpaceName &&
               Name == other.Name &&
               StaticConstructor.Equals(other.StaticConstructor) &&
               GenerateSetters == other.GenerateSetters &&
               GenerateBuilder == other.GenerateBuilder &&
               GenerateDefaultConstructor == other.GenerateDefaultConstructor &&
               HasDefaultContructor == other.HasDefaultContructor &&
               Properties.SequenceEqual(other.Properties);
    }

    public override int GetHashCode()
    {
        int hashCode = -1992365931;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NameSpaceName);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
        hashCode = hashCode * -1521134295 + EqualityComparer<Builder>.Default.GetHashCode(StaticConstructor);
        hashCode = hashCode * -1521134295 + EqualityComparer<IEnumerable<IProperty>>.Default.GetHashCode(Properties);
        hashCode = hashCode * -1521134295 + GenerateSetters.GetHashCode();
        hashCode = hashCode * -1521134295 + GenerateBuilder.GetHashCode();
        hashCode = hashCode * -1521134295 + GenerateDefaultConstructor.GetHashCode();
        hashCode = hashCode * -1521134295 + HasDefaultContructor.GetHashCode();
        return hashCode;
    }
}
