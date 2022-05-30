using Kruise.DataAccess.Postgres.Entities;
using Kruise.Domain;

namespace Kruise.DataAccess.Postgres.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly KruiseDbContext _dbContext;

        public PostsRepository(KruiseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<long> Add(Domain.Post newPost)
        {
            var post = new Entities.Post { Title = newPost.Title };
            _dbContext.Posts.Add(post);
            await _dbContext.SaveChangesAsync();
            return post.Id;
        }
    }
}
