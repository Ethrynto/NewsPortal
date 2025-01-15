using System.ComponentModel.DataAnnotations;

namespace API.Contracts;

public record CreateCommentRequest(
    [Required] string Content,
    [Required] Guid PostId);