using Domain.Models;
using Domain.Abstractions.Repositories;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AuthRepository(ApplicationDbContext context) : IAuthRepository
{
    public async Task Add(User user)
    {
        try
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error while adding account");
        }
    }

    public async Task<User> GetById(Guid id)
    {
        var user = await context.Users
            .Where (u => u.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (user != null)
            return user;
        
        throw new NotFoundException();
    }

    public async Task<User> GetByUsername(string username)
    {
        var user = await context.Users
            .Where (u => u.Username == username)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (user != null)
            return user;
        
        throw new NotFoundException();
    }
}