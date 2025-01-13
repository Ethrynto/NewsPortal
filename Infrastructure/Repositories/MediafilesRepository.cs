using Domain.Abstractions.Repositories;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MediafilesRepository(ApplicationDbContext context) : IMediafilesRepository
{
    public async Task<IEnumerable<Mediafile>> GetAllAsync()
    {
        return await context.Mediafiles.ToListAsync();
    }

    public async Task<Mediafile?> GetByIdAsync(Guid id)
    {
        return await context.Mediafiles.FindAsync(id);
    }

    public async Task AddAsync(Mediafile mediafile)
    {
        await context.Mediafiles.AddAsync(mediafile);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Mediafile mediafile)
    {
        context.Mediafiles.Update(mediafile);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var mediafile = await context.Mediafiles.FindAsync(id);
        if (mediafile != null)
        {
            context.Mediafiles.Remove(mediafile);
            await context.SaveChangesAsync();
        }
        else
        {
            throw new NotFoundException("Media file not found");
        }
    }
}