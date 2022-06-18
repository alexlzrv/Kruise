using AutoMapper;
using CSharpFunctionalExtensions;
using Kruise.Domain;
using Kruise.Domain.Interfaces;
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

        public async Task<long> Add(Domain.PostModel newPost)
        {
            var post = new Entities.PostEntity(0, newPost.Title);
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

        public async Task<Domain.PostModel[]> Get()
        {
            var posts = await _dbContext.Posts.AsNoTracking().ToArrayAsync();
            return _mapper.Map<Entities.PostEntity[], Domain.PostModel[]>(posts);
        }

        public async Task<Domain.PostModel?> Get(long postId)
        {
            var post = await _dbContext.Posts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == postId);
            if (post == null)
            {
                return null;
            }

            return _mapper.Map<Entities.PostEntity, PostModel>(post);
        }

        public async Task<Result> Update(long postId, PostModel post)
        {
            var postExists = await _dbContext.Posts.FirstOrDefaultAsync(x => x.Id == postId);
            if (postExists == null)
            {
                return Result.Failure($"Post with id: {postId} not found");
            }

            var postEntity = _mapper.Map(post, postExists);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
