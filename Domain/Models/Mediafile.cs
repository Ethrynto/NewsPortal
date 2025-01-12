namespace Domain.Models;

public class Mediafile
{
    public Guid Id { get; set; }
    public string? FileName { get; set; }
    public string? FileType { get; set; }
    public long FileSize { get; set; }
    public DateTime CreatedAt { get; set; }
}