using System.ComponentModel.DataAnnotations;

namespace AniMangaMVCCoreApp.Dtos.User;

public class UserCreateRequestDto
{
    [Required]
    public string? UserName { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required] // Can add password validation here
    public string? Password { get; set; }
}