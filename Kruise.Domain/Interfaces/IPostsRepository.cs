using CSharpFunctionalExtensions;

namespace Kruise.Domain.Interfaces
{
    public interface IPostsRepository
    {
        Task<long> Add(PostModel newPost);

        Task Remove(long postId);

        Task<PostModel[]> Get();

        Task<PostModel?> Get(long postiId);

        Task<Result> Update(long postId, PostModel post);
    }
}
