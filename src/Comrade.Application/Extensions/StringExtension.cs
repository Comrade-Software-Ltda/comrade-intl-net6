namespace Comrade.Application.Extensions;

public static class StringExtension
{
    public static string ToCamelCase(this string source)
    {
        var pattern =
            new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
        return new string(
            new CultureInfo("pt-BR", false)
                .TextInfo
                .ToTitleCase(
                    string.Join(" ", pattern.Matches(source))
                        .ToLower(CultureInfo.CurrentCulture)
                )
                .Replace(@" ", "", StringComparison.CurrentCulture)
                .Select((x, i) => i == 0 ? char.ToLower(x, CultureInfo.CurrentCulture) : x)
                .ToArray()
        );
    }

    public static string ToKebabCase(this string str)
    {
        var pattern =
            new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
        return string.Join("-", pattern.Matches(str)).ToLower(CultureInfo.CurrentCulture);
    }

    public static string ToSnakeCase(this string str)
    {
        var pattern =
            new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
        return string.Join("_", pattern.Matches(str)).ToLower(CultureInfo.CurrentCulture);
    }

    public static string ToPascalCase(this string source)
    {
        var pattern =
            new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
        return new string(
            new CultureInfo("pt-BR", false)
                .TextInfo
                .ToTitleCase(
                    string.Join(" ", pattern.Matches(source))
                        .ToLower(CultureInfo.CurrentCulture)
                )
                .Replace(@" ", "", StringComparison.CurrentCulture)
                .Select((x, i) => i == 0 ? char.ToUpper(x, CultureInfo.CurrentCulture) : x)
                .ToArray()
        );
    }

    public static string ToProperCase(this string source)
    {
        var cultureInfo = Thread.CurrentThread.CurrentCulture;
        var textInfo = cultureInfo.TextInfo;
        return textInfo.ToTitleCase(source);
    }

    public static string ToSlug(this string str)
    {
        str = Regex.Replace(str, @"\s+", "-");
        str = Regex.Replace(str ?? string.Empty, "([a-z])([A-Z])", "$1-$2")
            .ToLower(CultureInfo.CurrentCulture);

        return str;
    }

    public static int ToInt32(this string s)
    {
        _ = int.TryParse(s, out var i);
        return i;
    }

    public static double ToInt64(this string s)
    {
        _ = long.TryParse(s, out var i);
        return i;
    }

    public static decimal ToDecimal(this string s)
    {
        _ = decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var i);
        return i;
    }

    public static DateTime ToDoubleToDateTime(this string s)
    {
        var dateTime = DateTime.FromOADate(s.ToInt64());
        return dateTime;
    }
}
