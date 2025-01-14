using Domain.Abstractions.Repositories;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PostsRepository(ApplicationDbContext context) : IPostsRepository
{
    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        return await context.Posts.ToListAsync();
    }

    public async Task<Post> GetByIdAsync(Guid id)
    {
        return await context.Posts
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync() ?? throw new NotFoundException();
    }

    public async Task<Post> CreateAsync(Post post)
    {
        post.Id = Guid.NewGuid();
        context.Posts.Add(post);
        await context.SaveChangesAsync();
        return post;
    }

    public async Task<Post> UpdateAsync(Post post)
    {
        var existingPost = await context.Posts.FindAsync(post.Id);
        if (existingPost == null)
        {
            throw new NotFoundException("Post not found.");
        }

        context.Entry(existingPost).CurrentValues.SetValues(post);
        await context.SaveChangesAsync();
        return post;
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var post = await GetByIdAsync(id);
        context.Posts.Remove(post);
        await context.SaveChangesAsync();
    }
}