using TaskTracker.Application.Model.UserModels;

namespace TaskTracker.Application.Abstractions.Authentication;

public interface IAuthService
{
    public Task<UserResponseDto> Register(UserRegisterDto userResgiterDto);

    public Task<UserResponseDto> Login(UserLoginDto userLoginDto);

    public Task<IList<UserResponseDto>> GetUsers();
}
