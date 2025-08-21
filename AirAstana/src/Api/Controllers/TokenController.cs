using Application.Contracts.Services.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class TokenController : ControllerBase
{
    private readonly ITokenService _token;

    public TokenController(ITokenService token)
    {
        _token = token;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequest req, CancellationToken ct)
    {
        var result = await _token.TokenAsync(req.Username, req.Password, ct);
        return result is null ? Unauthorized(new { error = "Неправильный логин/пароль" }) : Ok(result);
    }
}