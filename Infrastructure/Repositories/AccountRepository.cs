using Domain.Models;
using Domain.Abstractions.Repositories;
using Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AccountRepository(ApplicationDbContext context) : IAccountRepository
{
    public async Task<bool> Add(Account account)
    {
        context.Users.Add(new User()
        {
            Id = account.Id,
            Username = account.Username,
            Email = account.Email,
            Password = account.PasswordHash
        });
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<Account> GetById(Guid id)
    {
        var user = await context.Users
            .Where (u => u.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (user != null)
            return new Account()
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                PasswordHash = user.Password,
            };
        
        throw new NotFoundException();
    }

    public Task<Account> GetByUsername(string username)
    {
        throw new NotImplementedException();
    }
}