using Domain.Abstractions.Services;
using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController(ICommentsService commentsService, ILogger<CommentsController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
    {
        return Ok(await commentsService.GetAllAsync());
    }
}