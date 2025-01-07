using API.Contracts;
using Application.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController(AccountService accountService): ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(string username, string password)
    {
        return Ok();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]RegisterAccountRequest request)
    {
        accountService.Register(request.Username, request.Email, request.Password);
        return Ok();
    }
}