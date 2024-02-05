using System.Reflection;

namespace Grenat.Functional.DDD.Generators.Src.Extensions;

public static class EnumExtensions
{
    public static T GetAttribute<T>(this Enum value) where T : Attribute
    {
        Type type = value.GetType();
        string name = Enum.GetName(type, value);
        if (name != null)
        {
            FieldInfo field = type.GetField(name);
            if (field != null)
            {
                T attr = Attribute.GetCustomAttribute(
                    field,
                    typeof(T)) as T;

                return attr;
            }
        }
        return null;
    }
}
