using Application.Contracts.Services;
using Application.Models.DTO;
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
    public async Task<IActionResult> GetAllFLightsAsync()
    {
        return Ok(await _flightService.GetAllAsync());
    }
    
    /// <summary>
    /// Получение рейса по его идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetFlightByIdAsync(int id)
    {
        var result = await _flightService.GetByIdAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// Создание нового рейса
    /// </summary>
    /// <param name="flight"></param>
    /// <returns></returns>
    [HttpPost]
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
    [HttpPut]
    public async Task<IActionResult> UpdateFlightAsync(FlightDto flight)
    {
        var result = await _flightService.UpdateFlightAsync(flight);
        return Ok(result);
    }
    
    /// <summary>
    /// Удаление рейса
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFlightAsync(int id)
    {
        var result = await _flightService.DeleteFlightAsync(id);
        return Ok(result);
    }
}