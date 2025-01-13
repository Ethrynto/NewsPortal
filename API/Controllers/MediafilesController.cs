using Domain.Abstractions.Services;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MediafilesController(IMediafilesService mediafilesService) : ControllerBase
{
    // GET: api/Mediafiles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Mediafile>>> GetImages()
    {
        var images = await mediafilesService.GetAllAsync();
        return Ok(images);
    }

    // GET: api/Mediafiles/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetImage(Guid id)
    {
        var mediafile = await mediafilesService.GetByIdAsync(id);
        if (mediafile == null)
            return NotFound();
        return Ok(mediafile);
    }

    // POST: api/Mediafiles
    [HttpPost]
    public async Task<IActionResult> PostImage(IFormFile? file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Invalid file.");
        }
        
        return Ok(await mediafilesService.AddAsync(file));
    }


    // PUT: api/Mediafiles/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutImage(Guid id, Mediafile image)
    {
        if (id != image.Id)
        {
            return BadRequest();
        }

        await mediafilesService.UpdateAsync(image);
        return NoContent();
    }

    // DELETE: api/Mediafiles/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteImage(Guid id)
    {
        await mediafilesService.DeleteAsync(id);
        return NoContent();
    }
}