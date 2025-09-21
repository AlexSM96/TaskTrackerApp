using TaskTracker.Application.Model.UserModels;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Extensions.Mappers;

public static class UserMapper
{
    public static UserResponseDto ToResponseDto(this UserEntity userEntity, string[] roles)
    {
        return new UserResponseDto()
        {
            Id = userEntity.Id,
            Roles = roles,
            Email = userEntity.Email!,
            Username = userEntity.UserName!,
            FIO = userEntity.FIO
        };
    }

    public static UserDto ToDto(this UserEntity userEntity)
    {
        return new UserDto(
            userEntity.Id, 
            userEntity.FIO, 
            userEntity.Email ?? "Email не указан"
            );
    }
}
