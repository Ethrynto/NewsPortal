using Domain.Abstractions.Repositories;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CommentsRepository(ApplicationDbContext context) : ICommentsRepository
{
    public async Task<IEnumerable<Comment>> GetAllAsync()
    {
        return await context.Comments.ToListAsync();
    }

    public async Task<Comment> GetByIdAsync(Guid id)
    {
        return await context.Comments
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync() ?? throw new NotFoundException();
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        await context.Comments.AddAsync(comment);
        await context.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment> UpdateAsync(Comment comment)
    {
        context.Entry(comment).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return comment;
    }

    public async Task DeleteAsync(Guid id)
    {
        var comment = await context.Comments.FindAsync(id);
        if (comment != null)
        {
            context.Comments.Remove(comment);
            await context.SaveChangesAsync();
        }
        else
        {
            throw new NotFoundException("Comment not found");
        }
    }
}