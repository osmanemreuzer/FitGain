using System.ComponentModel.DataAnnotations;
using FitGain.Entity;

namespace FitGain.Models;

public class PostCreateViewModel
{
    public int PostId { get; set; }
    [Required]
    [Display(Name = "Title")]
    public string Title { get; set; } = null!;
    [Required]
    public string? Description { get; set; } = null!;
    [Required]
    public string? Content { get; set; } = null!;
    [Required]
    public string? Url { get; set; } = null!;
    public bool IsActive { get; set; }

    public List<Tag> Tags { get; set; } = new(); 
}