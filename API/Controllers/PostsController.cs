using API.Contracts;
using Domain.Abstractions.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController(IPostsService postsService, ILogger<PostsController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPostsAsync()
    {
        logger.LogInformation("Get all posts");
        return Ok(await postsService.GetAllAsync());
    }

    // public async Task<IActionResult> GetPostsAsync(int page, int pageSize)
    // {
    //     logger.LogInformation("Get all posts");
    //     return Ok(await postsService.GetAllAsync());
    // }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostAsync(Guid id)
    {
        logger.LogInformation("Get post by id {id}", id);
        return Ok(await postsService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreatePostAsync([FromBody] CreatePostRequest request)
    {
        logger.LogInformation("Create post");
        Post post = new Post()
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Content = request.Content,
            CategoryId = request.CategoryId,
        };
        logger.LogInformation("Create post with id {id}", post.Id);
        return Ok(await postsService.CreateAsync(post));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePostAsync([FromQuery] Guid id, [FromBody] UpdatePostRequest request)
    {
        Post post = await postsService.GetByIdAsync(id);
        if(request.Title != String.Empty) post.Title = request.Title;
        if(request.Content != String.Empty) post.Content = request.Content;
        post.UpdatedAt = DateTime.UtcNow;
        logger.LogInformation("Update post by id {id}", id);
        return Ok(await postsService.UpdateAsync(post));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePostAsync([FromQuery] Guid id)
    {
        await postsService.DeleteAsync(id);
        logger.LogInformation("Delete post by id {id}", id);
        return Ok();
    }
}