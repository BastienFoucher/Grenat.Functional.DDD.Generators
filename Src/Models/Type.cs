namespace Grenat.Functional.DDD.Generators.Models
{
    public record TypeData : IType
    {
        public TypeData(string typeName)
        {
            TypeName = typeName;
        }

        public string TypeName { get; }
    }
}
