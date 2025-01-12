using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class AuthService(IAuthRepository authRepository, JwtService jwtService) : IAuthService
{
    public async Task Register(string username, string email, string password)
    {
        User user = new User()
        {
            Id = Guid.NewGuid(),
            Username = username,
            Email = email,
            Role = "default"
        };
        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, password);
        await authRepository.Add(user);
    }
    
    public async Task<string> Login(string username, string password)
    {
        User user = await authRepository.GetByUsername(username);
        var isCorrect = new PasswordHasher<User>().
            VerifyHashedPassword(user, user.PasswordHash, password);

        if (isCorrect == PasswordVerificationResult.Success)
        {
            return jwtService.GenerateToken(user);
        }
        else
        {
            throw new InvalidPasswordException();
        }
    }
}