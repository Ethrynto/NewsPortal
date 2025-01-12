namespace Domain.Models;

// ReSharper disable once ClassNeverInstantiated.Global
public class Category
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
}