using Domain.Models;

namespace Domain.Abstractions.Repositories;

public interface IAuthRepository
{
    Task Add(User user);
    Task<User> GetById(Guid id);
    Task<User> GetByUsername(string username);
}