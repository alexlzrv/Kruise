using CSharpFunctionalExtensions;

namespace Kruise.Domain;

public record PostModel
{
    public const int MaxTitleLength = 300;

    private PostModel(long id, string title)
    {
        Id = id;
        Title = title;
    }

    public long Id { get; }
    public string Title { get; }

    public static Result<PostModel> Create(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Result.Failure<PostModel>(Errors.Post.TitleCanNotBeNullOrWhiteSpace);
        }

        if (title.Length > MaxTitleLength)
        {
            var error = string.Format(Errors.Post.TitleMaxLength, MaxTitleLength);
            return Result.Failure<PostModel>(error);
        }

        return new PostModel(0, title);
    }
}
