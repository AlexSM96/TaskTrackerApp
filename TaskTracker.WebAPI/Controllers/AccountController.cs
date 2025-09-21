using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Abstractions.Authentication;
using TaskTracker.Application.Model.UserModels;

namespace TaskTracker.WebAPI.Controllers;

[Route("accounts")]
public class AccountController(IAuthService authService) : ApiBaseController
{
    private readonly IAuthService _authService = authService;

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto userLoginDto)
    {
        var result = await _authService.Login(userLoginDto);
        if(result is null)
        {
            return Unauthorized();
        }

        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
    {
        var result = await _authService.Register(userRegisterDto);
        return Ok(result);
    }


    [Authorize]
    [HttpGet("getusers")]
    public async Task<IActionResult> GetUsers()
    {
        var result = await _authService.GetUsers();
        return Ok(result);
    }
}
