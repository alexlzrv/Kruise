using AutoMapper;
using CSharpFunctionalExtensions;
using Kruise.DataAccess.Postgres.Entities;
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

        public async Task<long> Add(PostModel newPost)
        {
            var post = new PostEntity(0, newPost.Title);
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

        public async Task<PostModel[]> Get()
        {
            var posts = await _dbContext.Posts.AsNoTracking().ToArrayAsync();
            return _mapper.Map<PostEntity[], PostModel[]>(posts);
        }

        public async Task<PostModel?> Get(long postId)
        {
            var post = await _dbContext.Posts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == postId);
            if (post == null)
            {
                return null;
            }

            return _mapper.Map<PostEntity, PostModel>(post);
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
