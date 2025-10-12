using Microsoft.AspNetCore.Identity;

namespace TaskTracker.Domain.Entities;

public class UserEntity : IdentityUser<long>
{
    public string FIO { get; set; }

    public string Photo { get; set; }
}
