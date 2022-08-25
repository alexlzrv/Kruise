using Kruise.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kruise.API.Controllers;

[ApiController]
[Route("api/posts")]
public class PublishController : ControllerBase
{
    private readonly ILogger<PublishController> _logger;
    private readonly IPublishService _publish;
    private readonly IPostsRepository _repository;

    public PublishController(ILogger<PublishController> logger, IPublishService publish, IPostsRepository repository)
    {
        _logger = logger;
        _publish = publish;
        _repository = repository;
    }

    [HttpPost("{postId}/publication")]
    public async Task<IActionResult> Publish(long postId)
    {
        var post = await _repository.Get(postId);
        var senders = _publish.SendPost(post);

        return Ok();
    }
}
