using System.ComponentModel.DataAnnotations;
using Kruise.Domain;

namespace Kruise.DataAccess.Postgres.Entities;

public class PostEntity
{
    public PostEntity(long id, string title)
    {
        Id = id;
        Title = title;
    }

    public long Id { get; set; }

    [StringLength(PostModel.MaxTitleLength, ErrorMessage = Errors.Post.TitleMaxLength)]
    public string Title { get; set; }
}
