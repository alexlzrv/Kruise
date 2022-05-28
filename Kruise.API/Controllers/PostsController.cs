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

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Post>>> GetPostsItems()
        //{
        //    return await _repository.Posts.ToListAsync();
        //}

        //[HttpGet("{postId}")]
        //public async Task<ActionResult<Post>> GetPostsItem(long postId)
        //{
        //    var postsItem = await _repository.Posts.FindAsync(postId);

        //    if (postsItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return postsItem;
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Post>> DeletePostsItem(long id)
        //{
        //    var todoItem = await _repository.Posts.FindAsync(id);
        //    if (todoItem == null)
        //    {
        //        return NotFound();
        //    }

        //    _repository.Posts.Remove(todoItem);
        //    await _repository.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}
