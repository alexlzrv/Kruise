namespace Kruise.Domain
{
    public interface IPostsRepository
    {
        Task<long> Add(Post newPost);
    }
}
