namespace Domain.Abstractions.Services;

public interface IAuthService
{
    Task Register(string username, string email, string password);
    Task<string> Login(string username, string password);
    
}