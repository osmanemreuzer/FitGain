namespace FitGain.Entity;

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string? Image { get; set; } 
    public DateTime PublishedOn { get; set; } 
    public bool IsActive { get; set; } 
    public int UserId { get; set; }
    public User User { get; set; } = null!; //bir post bir usera ait olcak
    public List<Tag> Tags { get; set; } = new List<Tag>();
    public List<Comment> Comments { get; set; } = new List<Comment>();
}