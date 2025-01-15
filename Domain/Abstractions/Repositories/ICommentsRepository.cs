using Domain.Models;

namespace Domain.Abstractions.Repositories;

public interface ICommentsRepository
{
    Task<IEnumerable<Comment>> GetAllAsync();
    Task<Comment> GetByIdAsync(Guid id);
    Task<Comment> CreateAsync(Comment comment);
    Task<Comment> UpdateAsync(Comment comment);
    Task DeleteAsync(Guid id);
}