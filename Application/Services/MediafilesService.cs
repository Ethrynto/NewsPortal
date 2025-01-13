using System.Net.Mime;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class MediafilesService(IMediafilesRepository mediafilesRepository, IWebHostEnvironment environment) : IMediafilesService
{
    public async Task<IEnumerable<Mediafile>> GetAllAsync()
    {
        return await mediafilesRepository.GetAllAsync();
    }

    public async Task<Mediafile?> GetByIdAsync(Guid id)
    {
        var mediafile = await mediafilesRepository.GetByIdAsync(id);
        return mediafile ?? throw new NotFoundException();
    }

    public async Task<Mediafile> AddAsync(IFormFile? file)
    {
        if (environment == null)
        {
            throw new InvalidOperationException("Environment is not initialized.");
        }
        if(file == null)
            throw new ArgumentNullException(nameof(file));

        Guid id = Guid.NewGuid();
        string fileName = $"{id}{Path.GetExtension(file.FileName)}";
        string filePath = Path.Combine(environment.WebRootPath, "uploads", fileName);

        Console.WriteLine($"Saving file to: {filePath}");

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var mediafile = new Mediafile()
        {
            Id = id,
            FileName = fileName,
            FileType = file.ContentType,
            FileSize = file.Length,
            CreatedAt = DateTime.UtcNow
        };

        await mediafilesRepository.AddAsync(mediafile);
        return mediafile;
    }


    public async Task UpdateAsync(Mediafile image)
    {
        var entity = new Mediafile()
        {
            Id = image.Id,
            FileName = image.FileName,
            FileType = image.FileType,
            FileSize = image.FileSize,
            CreatedAt = image.CreatedAt
        };
        await mediafilesRepository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        await mediafilesRepository.DeleteAsync(id);
    }
    
    private string GetFileUrl(string fileName)
    {
        return Path.Combine("http://localhost:8080/uploads", fileName);
    }
}