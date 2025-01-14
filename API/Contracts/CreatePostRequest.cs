using System.ComponentModel.DataAnnotations;

namespace API.Contracts;

public record CreatePostRequest(
    [Required] string Title,
    [Required] string Content,
    [Required] Guid CategoryId);