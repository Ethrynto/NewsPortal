using API.Models;
using Application.Abstractions.Repositories;
using Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AccountRepository(ApplicationDbContext context) : IAccountRepository
{
    public void Add(string username, string email, string password)
    {
        throw new NotImplementedException();
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