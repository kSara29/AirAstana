namespace Application.Models.DTO;

public class FlightDto
{
    /// <summary>
    /// Идентификайионный номер
    /// </summary>
    public int? Id { get; set; }
    
    /// <summary>
    /// Пункт вылета
    /// </summary>
    public string Origin { get; set; } = null!;
    
    /// <summary>
    /// Пункт назначения
    /// </summary>
    public string Destination { get; set; } = null!;
    
    /// <summary>
    /// Время вылета
    /// </summary>
    public DateTimeOffset Departure { get; set; }
    
    /// <summary>
    /// Время прилета
    /// </summary>
    public DateTimeOffset Arrival { get; set; }
    
    /// <summary>
    /// Статус рейса
    /// </summary>
    public string Status { get; set; } = string.Empty;
}