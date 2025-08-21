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
    public async Task<IActionResult> GetAllFLightsAsync()
    {
        return Ok(await _flightService.GetAllAsync());
    }

    /// <summary>
    /// Создание нового рейса
    /// </summary>
    /// <param name="flight"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Moderator")]
    public async Task<IActionResult> CreateFlightAsync(FlightDto flight)
    {
        var result = await _flightService.CreateFlightAsync(flight);
        return Ok(result);
    }
    
    /// <summary>
    /// Обновление рейса
    /// </summary>
    /// <param name="flight"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Moderator")]
    public async Task<IActionResult> UpdateFlightAsync(int id, [FromBody] EditFlightDto flightStatus, CancellationToken ct)
    {
        var result = await _flightService.UpdateFlightAsync(id, flightStatus);
        return Ok(result);
    }
}