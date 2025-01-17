using System.ComponentModel.DataAnnotations;

namespace API.Contracts;

public record UpdateCommentRequest(
    [Required] string Content);