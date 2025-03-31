using System.ComponentModel.DataAnnotations;

namespace QuizApp.API.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = string.Empty; // "Admin" or "Developer"

    public string? Department { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}