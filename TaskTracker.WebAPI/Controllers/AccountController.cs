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


   // [Authorize]
    [HttpGet("getusers")]
    public async Task<IActionResult> GetUsers()
    {
        var result = await _authService.GetUsers();
        return Ok(result);
    }

    [HttpPost("upload-photo")]
    public async Task<IActionResult> UploadPhoto([FromForm] string email, IFormFile file)
    {
        if (file == null || file.Length == 0) return BadRequest("Файл не выбран");

        var currentDir = Directory.GetCurrentDirectory();
        string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "..\\ReactApp\\tasktracker-app\\src\\images");
        if (!Directory.Exists(uploadsFolder))
        {
            return BadRequest(new { Error = "Не найден путь для сохранения файла" });
        }


        var filePath = Path.Combine(uploadsFolder, file.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }


        bool success = await authService.SaveUserPhoto(email, $"src\\images\\{file.FileName}");
        if (!success) 
        {
            return BadRequest(new { Error = "Не удалось привязать фото к текущему пользователю" });
        }

        return Ok(new { Message = "Файл успешно загружен" });
    }
}
