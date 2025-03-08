using TaskTracker.Application.Model.UserModels;
using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Models;

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
        };
    }

}
