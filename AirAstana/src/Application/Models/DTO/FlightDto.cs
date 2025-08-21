namespace Application.Models.DTO;

public class FlightDto
{
    public string? Origin { get; set; }
    public string? Destination { get; set; }
    public string Order { get; set; } = "asc"; 
}