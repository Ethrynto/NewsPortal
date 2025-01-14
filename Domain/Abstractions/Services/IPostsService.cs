using Domain.Models;

namespace Domain.Abstractions.Services;

public interface IPostsService
{
    Task<IEnumerable<Post>> GetAllAsync();
    Task<Post> GetByIdAsync(Guid id);
    Task<Post> CreateAsync(Post post);
    Task<Post> UpdateAsync(Post post);
    Task DeleteAsync(Guid id);
}