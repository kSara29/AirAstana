namespace Application.Cache;

public class CacheKeys
{
    private const string Prefix = "flights";
    public static string FlightsAll => $"{Prefix}:all";
    public static string FlightById(int id) => $"{Prefix}:byId:{id}";

    public static string FlightsList(string? origin, string? destination, bool desc)
    {
        var o = Normalize(origin);
        var d = Normalize(destination);
        var order = desc ? "desc" : "asc";
        return $"{Prefix}:list:o={o}:d={d}:a={order}";
    }

    private static string Normalize(string? s)
        => string.IsNullOrWhiteSpace(s) ? "_" : s.Trim().ToLowerInvariant();
}