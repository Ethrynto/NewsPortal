using API.Contracts;
using Application.Services;
using Domain.Abstractions.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IAuthService authService, ILogger<AuthController> logger)
    : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        return Ok(await authService.Login(request.Username, request.Password));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        try
        {
            await authService.Register(request.Username, request.Email, request.Password);
            logger.LogInformation("The user {Username} successfully registered", request.Username);
            return Ok(await authService.Login(request.Username, request.Password));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "The error by user registration {Username}", request.Username); 
            return StatusCode(500, "Server include errors.");
        }
    }
}