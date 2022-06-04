using AutoMapper;
using Kruise.Domain;
using Microsoft.EntityFrameworkCore;

namespace Kruise.DataAccess.Postgres.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly KruiseDbContext _dbContext;
        private readonly IMapper _mapper;

        public PostsRepository(KruiseDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<long> Add(Domain.Post newPost)
        {
            var post = new Entities.Post { Title = newPost.Title };
            _dbContext.Posts.Add(post);
            await _dbContext.SaveChangesAsync();
            return post.Id;
        }

        public async Task Remove(long postId)
        {
            var post = await _dbContext.Posts.FindAsync(postId);
            _dbContext.Posts.Remove(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Domain.Post[]> Get()
        {
            var posts = await _dbContext.Posts.AsNoTracking().ToArrayAsync();
            return _mapper.Map<Entities.Post[], Domain.Post[]>(posts);
        }

        public async Task<Domain.Post?> Get(long postId)
        {
            var post = await _dbContext.Posts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == postId);
            if (post == null)
            {
                return null;
            }

            return _mapper.Map<Entities.Post, Domain.Post>(post);
        }
    }
}
