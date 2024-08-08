using System.ComponentModel.DataAnnotations;

namespace API;

public class RegisterDTO
{
    [Required]
    public string username { get; set; } = string.Empty;
    [Required]
    [StringLength(16, MinimumLength = 8)]
    public string password { get; set; } = string.Empty;
}
