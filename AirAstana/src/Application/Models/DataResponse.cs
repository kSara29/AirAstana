namespace Application.Models;

public class DataResponse
{
    public int CompletedCount { get; set; }
    public int ErrorCount { get; set; }
    public List<string> Errors { get; set; } = new();
}