using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Models;

namespace Application.Services;

public class CommentsService(ICommentsRepository repository) : ICommentsService
{
    public async Task<IEnumerable<Comment>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<Comment> GetByIdAsync(Guid id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        return await repository.CreateAsync(comment);
    }

    public async Task<Comment> UpdateAsync(Comment comment)
    {
        return await repository.UpdateAsync(comment);
    }

    public async Task DeleteAsync(Guid id)
    {
        await repository.DeleteAsync(id);
    }
}