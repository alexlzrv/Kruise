namespace Kruise.Domain
{
    public interface IPostsRepository
    {
        Task<long> Add(Post newPost);

        Task RemovePost(long id);

        IEnumerable<Post> GetPosts();

        Task<Post> GetPostById(long id);
    }
}
