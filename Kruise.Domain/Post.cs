using CSharpFunctionalExtensions;

namespace Kruise.Domain;

public record Post
{
    public const int MaxTitleLength = 300;

    private Post(long id, string title)
    {
        Id = id;
        Title = title;
    }

    public long Id { get; }

    public string Title { get; }

    public static Result<Post> Create(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Result.Failure<Post>(Errors.Post.TitleCanNotBeNullOrWhiteSpace);
        }

        if (title.Length > MaxTitleLength)
        {
            var error = string.Format(Errors.Post.TitleMaxLength, MaxTitleLength);
            return Result.Failure<Post>(error);
        }

        return new Post(0, title);
    }
}
