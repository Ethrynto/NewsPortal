using System.ComponentModel.DataAnnotations;

namespace API.Contracts;

public record UpdatePostRequest(
    string Title,
    string Content);