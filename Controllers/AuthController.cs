using Login.Application.DTOs;
using Login.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterDTO dto)
    {
        var result = await _authService.RegisterAsync(dto);

        if (result == null) return BadRequest("User already exists or invalid data");

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginDTO dto)
    {
        var token = await _authService.LoginAsync(dto);

        if (token == null) return Unauthorized("Invalid e-mail or password");

        return Ok(new { token });
    }
}
