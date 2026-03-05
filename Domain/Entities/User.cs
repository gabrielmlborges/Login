using Login.Domain.Enums;

namespace Login.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool Active { get; set; } = true;
}
