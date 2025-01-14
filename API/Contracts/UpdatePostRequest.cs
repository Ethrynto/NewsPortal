using System.ComponentModel.DataAnnotations;

namespace API.Contracts;

public record UpdatePostRequest(
    [Required] Guid Id,
    string Title,
    string Content);