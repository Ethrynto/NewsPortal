using Domain.Models;

namespace Domain.Abstractions.Repositories;

public interface IAccountRepository
{
    Task<bool> Add(Account account);
    Task<Account> GetById(Guid id);
    Task<Account> GetByUsername(string username);
}