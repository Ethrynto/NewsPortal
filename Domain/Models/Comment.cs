﻿namespace Domain.Models;

public class Comment
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public Guid PostId { get; set; }
    public Post? Post { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public DateTime CreatedAt { get; set; }
}