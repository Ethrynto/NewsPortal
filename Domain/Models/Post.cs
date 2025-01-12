namespace Domain.Models;

public class Post
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Content { get; set; }
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    public IEnumerable<Comment>? Comments { get; set; }
    public DateTime PublishedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}