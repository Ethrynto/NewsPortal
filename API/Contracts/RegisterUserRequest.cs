﻿using System.ComponentModel.DataAnnotations;

namespace API.Contracts;

public record RegisterUserRequest(
    [Required] string Username, 
    [Required] string Email, 
    [Required] string Password);