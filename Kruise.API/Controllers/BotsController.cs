using Kruise.API.Telegram;
using Kruise.Domain;
using Kruise.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kruise.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BotsController : ControllerBase
{
    private readonly ILogger<PostsController> _logger;
    private readonly IPostsRepository _repository;

    public BotsController(ILogger<PostsController> logger, IPostsRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpPost("{postId}")]
    public async Task<IActionResult> Post([FromServices] TelegramService telegramService, long postId)
    {
        var post = await _repository.Get(postId);

        if (post == null)
        {
            await telegramService.SendExceptionPost($"Post with Id:{postId} not found");
            return NotFound();
        }

        await telegramService.SendPost(post);
        return Ok();
    }
}
