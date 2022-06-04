namespace Kruise.Domain
{
    public interface IPostsRepository
    {
        Task<long> Add(Post newPost);

        Task Remove(long postId);

        Task<Post[]> Get();

        Task<Post?> Get(long postiId);
    }
}
