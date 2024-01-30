namespace Grenat.Functional.DDD.Generators.Src.Extensions;

public static class StringExtensions
{
    public static string ToLowerFirstChar(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return char.ToLower(input[0]) + input.Substring(1);
    }

    public static string ToUpperFirstChar(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return char.ToUpper(input[0]) + input.Substring(1);
    }

    public static string RemoveLastChars(this string input, int numberOfChars = 1)
    {
        if (numberOfChars > input.Length)
            return string.Empty;

        return input.Remove(input.Length - numberOfChars, numberOfChars);
    }
}
