using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Domain.Abstractions.Services;

public interface IMediafilesService
{
    Task<IEnumerable<Mediafile>> GetAllAsync();
    Task<Mediafile?> GetByIdAsync(Guid id);
    Task<Mediafile> AddAsync(IFormFile? image);
    Task UpdateAsync(Mediafile image);
    Task DeleteAsync(Guid id);
}