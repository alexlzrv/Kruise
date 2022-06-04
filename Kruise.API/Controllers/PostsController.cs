using Kruise.API.Contracts;
using Kruise.DataAccess.Postgres;
using Kruise.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var post = Post.Create(request.Title);
            if (post.IsFailure)
            {
                _logger.LogError(post.Error);
                return Problem(post.Error);
            }

            var postId = await _repository.Add(post.Value);
            return Ok(postId);
        }

        [HttpGet]
        public IEnumerable<Post> Get()
        {
            return _repository.GetPosts();
        }

        [HttpGet("{id}")]
        public async Task<Post> Get(long id)
        {
            return await _repository.GetPostById(id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _repository.RemovePost(id);
            return Ok();
        }
    }
}
