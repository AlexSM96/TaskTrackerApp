using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using TaskTracker.Application.Abstractions.Authentication;
using TaskTracker.Application.Extensions.Mappers;
using TaskTracker.Application.Model.UserModels;
using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Models;
using TaskTracker.Domain.Options;

namespace TaskTracker.Application.Services;

public class AuthService(
    IOptions<AuthOption> authOption,
    UserManager<UserEntity> userManager) : IAuthService
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly AuthOption _authOption = authOption.Value;

    public async Task<IList<UserResponseDto>> GetUsers()
    {
        var users = await _userManager.Users
            .AsNoTracking()
            .ToListAsync();

        List<UserResponseDto> userResponseDtos = [];
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userResponseDtos.Add(user.ToResponseDto(roles.ToArray()));
        }

        return userResponseDtos;
    }

    public async Task<UserResponseDto> Register(UserRegisterDto userResgiterDto)
    {
        var alredyREgisterdUser = await _userManager.FindByEmailAsync(userResgiterDto.Email);
        if (alredyREgisterdUser is not null)
        {
            throw new Exception("Already registered user " + userResgiterDto.Email);
        }

        var createdUser = await _userManager.CreateAsync(new UserEntity
        {
            Email = userResgiterDto.Email,
            UserName = Regex.Replace(userResgiterDto.Email, @"[^a-zA-Z]", ""),
            FIO = userResgiterDto.FIO
        }, userResgiterDto.Password);

        if (!createdUser.Succeeded)
        {
            throw new Exception($"Errors: {string.Join("\n", createdUser.Errors.Select(x => $"{x.Code} {x.Description}"))}");
        }

        var user = await _userManager.FindByEmailAsync(userResgiterDto.Email);
        if (user is null)
        {
            throw new Exception($"Not found user with email {userResgiterDto.Email}");
        }

        var result = await _userManager.AddToRoleAsync(user, Roles.User);

        if (!result.Succeeded)
        {
            throw new Exception($"Errors: {string.Join("\n", result.Errors.Select(x => $"{x.Code} {x.Description}"))}");
        }

        var roles = await _userManager.GetRolesAsync(user);
        return GenerateToken(user.ToResponseDto(roles.ToArray()));
    }

    public async Task<UserResponseDto> Login(UserLoginDto userLoginDto)
    {
        var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
        if(user is null)
        {
            throw new Exception($"User with Login {userLoginDto.Email} not registered");
        }

        var isValidPassword = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);

        if (!isValidPassword)
        {
            throw new Exception("Not valid password");
        }

        var roles = await _userManager.GetRolesAsync(user);
        return GenerateToken(user.ToResponseDto(roles.ToArray()));
    }

    public async Task<bool> SaveUserPhoto(string userEmail, string photoPath)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user is null)
        {
            throw new Exception($"User with Email {userEmail} not registered");
        }

        user.Photo = photoPath;
        var updatedUser = await _userManager.UpdateAsync(user);
        return updatedUser is not null && updatedUser.Succeeded;
    }

    private UserResponseDto GenerateToken(UserResponseDto userResposneDto)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_authOption.TokenPrivateKey);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = GenerateClaims(userResposneDto),
            Expires = DateTime.UtcNow.AddMinutes(_authOption.ExpiredIntervalMinutes),
            SigningCredentials = credentials,
        };

        var token = handler.CreateToken(tokenDescriptor);
        userResposneDto.Token = handler.WriteToken(token);

        return userResposneDto;
    }

    private ClaimsIdentity GenerateClaims(UserResponseDto userResponseDto)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.Name, userResponseDto.Email!));
        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, userResponseDto.Id.ToString()));

        foreach (var role in userResponseDto.Roles!)
        {
            if (!string.IsNullOrWhiteSpace(role))
            {
                claims.AddClaim(new Claim(ClaimTypes.Role, role));
            }
        }

        return claims;
    }
}
