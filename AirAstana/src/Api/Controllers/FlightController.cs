using Application.Contracts.Services;
using Application.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/flights")]
public class FlightController: ControllerBase
{
    private readonly IFlightService _flightService;

    public FlightController(IFlightService flightService)
    {
        _flightService = flightService;
    }
    
    /// <summary>
    /// Получение всех рейсов
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllFLightsAsync([FromQuery] FlightDto query)
    {
        var desc = query.Order.Equals("desc", StringComparison.OrdinalIgnoreCase);
        return Ok(await _flightService.GetAllAsync(query.Origin, query.Destination, desc));
    }

    /// <summary>
    /// Создание нового рейса
    /// </summary>
    /// <param name="createFlight">Рейс</param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Moderator")]
    public async Task<IActionResult> CreateFlightAsync(CreateFlightDto createFlight)
    {
        var result = await _flightService.CreateFlightAsync(createFlight);
        return Ok(result);
    }

    /// <summary>
    /// Обновление рейса
    /// </summary>
    /// <param name="id">Идентификационный номер</param>
    /// <param name="flightStatus">Новый статус рейса</param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Moderator")]
    public async Task<IActionResult> UpdateFlightAsync(int id, [FromBody] EditFlightDto flightStatus)
    {
        var result = await _flightService.UpdateFlightAsync(id, flightStatus);
        return Ok(result);
    }
}