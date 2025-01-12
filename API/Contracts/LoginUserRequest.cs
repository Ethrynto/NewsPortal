using System.ComponentModel.DataAnnotations;

namespace API.Contracts;

public record LoginUserRequest(
    [Required] string Username, 
    [Required] string Password);