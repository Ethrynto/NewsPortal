using API.Models;

namespace Application.Abstractions.Repositories;

public interface IAccountRepository
{
    void Add(string username, string email, string password);
    Task<Account> GetById(Guid id);
    Task<Account> GetByUsername(string username);
}