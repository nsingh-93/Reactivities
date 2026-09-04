using Microsoft.AspNetCore.Identity;

namespace Domain;

public class User : IdentityUser
{
    public string? UserDisplayName { get; set; }
    public string? Bio { get; set; }
    public string? ImageUrl { get; set; }
}
