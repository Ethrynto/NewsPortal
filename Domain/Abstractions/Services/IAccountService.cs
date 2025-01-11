namespace Domain.Abstractions.Services;

public interface IAccountService
{
    Task<bool> Register(string username, string email, string password);
    Task<bool> Login(string username, string password);
    
}