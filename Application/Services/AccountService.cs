using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class AccountService(IAccountRepository accountRepository) : IAccountService
{
    public async Task<bool> Register(string username, string email, string password)
    {
        Account account = new Account()
        {
            Id = Guid.NewGuid(),
            Username = username,
            Email = email
        };
        account.PasswordHash = new PasswordHasher<Account>().HashPassword(account, password);
        return await accountRepository.Add(account);
    }
    
    public async Task<bool> Login(string username, string password)
    {
        Account account = await accountRepository.GetByUsername(username);
        var isCorrect = new PasswordHasher<Account>().
            VerifyHashedPassword(account, account.PasswordHash, password);

        if (isCorrect == PasswordVerificationResult.Success)
        {
            // Generate token
        }
        else
        {
            throw new InvalidPasswordException();
        }
        return isCorrect > 0;
    }
}