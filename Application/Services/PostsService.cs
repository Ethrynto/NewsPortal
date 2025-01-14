using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Models;

namespace Application.Services;

public class PostsService(IPostsRepository repository) : IPostsService
{
    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<Post> GetByIdAsync(Guid id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task<Post> CreateAsync(Post post)
    {
        return await repository.CreateAsync(post);
    }

    public async Task<Post> UpdateAsync(Post post)
    {
        return await repository.UpdateAsync(post);
    }

    public async Task DeleteAsync(Guid id)
    {
        await repository.DeleteAsync(id);
    }
}