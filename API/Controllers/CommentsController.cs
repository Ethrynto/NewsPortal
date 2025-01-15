using API.Contracts;
using Application.Services;
using Domain.Abstractions.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController(ICommentsService commentsService, JwtService jwtService, ILogger<CommentsController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
    {
        return Ok(await commentsService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Comment>> GetComment(Guid id)
    {
        return Ok(await commentsService.GetByIdAsync(id));
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Comment>> PostComment(CreateCommentRequest request)
    {
        var token = Request.Headers["Authorization"].ToString();

        if (string.IsNullOrWhiteSpace(token))
        {
            return Unauthorized("No token provided");
        }

        var bearerToken = token.StartsWith("Bearer ") ? token.Substring(7) : token;
        Dictionary<string, string> userData = jwtService.Decode(bearerToken);
        if (!userData.ContainsKey("id")) return Unauthorized();
        
        Comment comment = new Comment()
        {
            Id = Guid.NewGuid(),
            Content = request.Content,
            PostId = request.PostId,
            UserId = userData.TryGetValue("id", out var value) ? Guid.Parse(value) : Guid.Empty,
            CreatedAt = DateTime.UtcNow,
        };
        return await commentsService.CreateAsync(comment);
    }
}