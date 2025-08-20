namespace Application.Cache;

public class CacheKeys
{
    public const string FlightsAll = "flights:all";
    public static string FlightById(int id) => $"flights:{id}";
}