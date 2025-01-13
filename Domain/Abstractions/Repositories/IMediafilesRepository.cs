using Domain.Models;

namespace Domain.Abstractions.Repositories;

public interface IMediafilesRepository
{
    Task<IEnumerable<Mediafile>> GetAllAsync();
    Task<Mediafile?> GetByIdAsync(Guid id);
    Task AddAsync(Mediafile mediafile);
    Task UpdateAsync(Mediafile mediafile);
    Task DeleteAsync(Guid id);
}