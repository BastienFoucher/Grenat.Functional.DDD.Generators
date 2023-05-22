namespace Grenat.Functional.DDD.Generators.Models;

public record StaticEntityConstructorParameter
{
    public string Name { get; }
    public string Type { get; }

    public StaticEntityConstructorParameter(string name, string type)
    {
        Name = name;
        Type = type;
    }
}

public sealed class StaticEntityConstructor : IEquatable<StaticEntityConstructor>
{
    public bool Any { get; }
    public string Name { get; }
    public string RecordName { get; }
    public string ReturningType { get; }

    public ImmutableList<StaticEntityConstructorParameter> Parameters { get; }

    public StaticEntityConstructor(bool any, string recordName, string name, ImmutableList<StaticEntityConstructorParameter> parameters, string returningType)
    {
        Any = any;
        Name = name;
        RecordName = recordName;
        Parameters = parameters;
        ReturningType = returningType;
    }

    public StringBuilder GenerateBuildMethod(ImmutableList<string> generatedBuilderFields)
    {
        var body = new StringBuilder();
        foreach (var parameter in Parameters)
        {
            var matchingPrivateField = generatedBuilderFields.
                FirstOrDefault(f => f.Remove(0, 1).ToLower() == parameter.Name.ToLower());

            // TODO : create a warning if no property could be matched to the parameter
            if (matchingPrivateField != null)
                body.Append($"{matchingPrivateField}, ");
        }

        return new StringBuilder().Append($@"
        public {ReturningType} Build() => {RecordName}.{Name}({body.ToString().RemoveLastChars(2)});
");
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as StaticEntityConstructor);
    }

    public bool Equals(StaticEntityConstructor other)
    {
        return other is not null &&
               Any == other.Any &&
               Name == other.Name &&
               RecordName == other.RecordName &&
               ReturningType == other.ReturningType &&
               Parameters.SequenceEqual(other.Parameters);
    }

    public override int GetHashCode()
    {
        int hashCode = 1833930171;
        hashCode = hashCode * -1521134295 + Any.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(RecordName);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ReturningType);
        return hashCode;
    }

    public static bool operator ==(StaticEntityConstructor left, StaticEntityConstructor right)
    {
        return EqualityComparer<StaticEntityConstructor>.Default.Equals(left, right);
    }

    public static bool operator !=(StaticEntityConstructor left, StaticEntityConstructor right)
    {
        return !(left == right);
    }
}
