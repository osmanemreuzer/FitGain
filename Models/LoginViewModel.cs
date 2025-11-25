using System.ComponentModel.DataAnnotations;

namespace FitGain.Models;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [StringLength(10,MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}