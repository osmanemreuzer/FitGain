namespace FitGain.Entity;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string? Image { get; set; } 
    public string? Name { get; set; } 
    public string? Email { get; set; } 
    public string? Password { get; set; } 
    public List<Post> Posts { get; set; } = new List<Post>(); //User birden fazla posts atabilir
    public List<Comment> Comments { get; set; } = new List<Comment>();
   
}