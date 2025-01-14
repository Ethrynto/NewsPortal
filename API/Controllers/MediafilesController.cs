using Domain.Abstractions.Services;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MediafilesController(IMediafilesService mediafilesService, ILogger<MediafilesController> logger) : ControllerBase
{
    // GET: api/Mediafiles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Mediafile>>> GetImages()
    {
        var images = await mediafilesService.GetAllAsync();
        logger.LogInformation($"Found {images.Count()} images");
        return Ok(images);
    }

    // GET: api/Mediafiles/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetImage(Guid id)
    {
        var mediafile = await mediafilesService.GetByIdAsync(id);
        if (mediafile == null)
        {
            logger.LogInformation($"Mediafile with id {id} not found");
            return NotFound();
        }
        logger.LogInformation($"Media file with id {id} found");
        return Ok(mediafile);
    }

    // POST: api/Mediafiles
    [HttpPost]
    public async Task<IActionResult> PostImage(IFormFile? file)
    {
        if (file == null || file.Length == 0)
        {
            logger.LogError("File is empty");
            return BadRequest("Invalid file.");
        }
        logger.LogInformation($"Media file successfully created");
        return Ok(await mediafilesService.AddAsync(file));
    }


    // PUT: api/Mediafiles/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutImage(Guid id, Mediafile image)
    {
        if (id != image.Id)
        {
            logger.LogError($"Media file with id {id} not match");
            return BadRequest();
        }

        await mediafilesService.UpdateAsync(image);
        logger.LogInformation($"Media file with id {id} updated");
        return NoContent();
    }

    // DELETE: api/Mediafiles/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteImage(Guid id)
    {
        await mediafilesService.DeleteAsync(id);
        logger.LogInformation($"Media file with id {id} deleted");
        return NoContent();
    }
}