using Kruise.API.Contracts;
using Kruise.Domain;
using Kruise.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kruise.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly ILogger<PostsController> _logger;
        private readonly IPostsRepository _repository;

        public PostsController(ILogger<PostsController> logger, IPostsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostRequest request)
        {
            var post = PostModel.Create(request.Title);
            if (post.IsFailure)
            {
                _logger.LogError(post.Error);
                return Problem(post.Error);
            }

            var postId = await _repository.Add(post.Value);
            return Ok(postId);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts = await _repository.Get();
            return Ok(posts);
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> Get(long postId)
        {
            var post = await _repository.Get(postId);
            if (post == null)
            {
                return NotFound($"Posts with Id:{postId} not found");
            }

            return Ok(post);
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> Delete(long postId)
        {
            await _repository.Remove(postId);
            return Ok();
        }

        [HttpPut("{postId}")]
        public async Task<IActionResult> Update(long postId, UpdatedPostRequest request)
        {
            var updatedPost = PostModel.Create(request.Title);
            if (updatedPost.IsFailure)
            {
                _logger.LogError(updatedPost.Error);
                return Problem(updatedPost.Error);
            }

            var updatedResult = await _repository.Update(postId, updatedPost.Value);
            if (updatedResult.IsFailure)
            {
                _logger.LogError(updatedResult.Error);
                return Problem(updatedResult.Error);
            }

            return Ok();
        }
    }
}
