namespace DATN.Data.Entities;

public class ApplicationUser
{
    public const string AdminRole = "Admin";
    public const string UserRole = "User";

    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string NormalizedEmail { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }

    public string PasswordHash { get; set; } = string.Empty;

    public string Role { get; set; } = UserRole;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? LastLoginAt { get; set; }
}
