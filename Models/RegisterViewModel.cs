using System.ComponentModel.DataAnnotations;

namespace FitGain.Models;

public class RegisterViewModel
{
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    [Display(Name = "Name Surname")]
    public string? Name { get; set; } 
    [Required]
    [EmailAddress]
    public string? Email { get; set; } 
    [Required]
    [StringLength(10, MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string? Password { get; set; } 
    [Required]
    [StringLength(10, MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Compare(nameof(Password),ErrorMessage = "Your password does not match.")]
    [Display(Name = "Confirm Password")]
    public string? ConfirmPassword { get; set; }

}