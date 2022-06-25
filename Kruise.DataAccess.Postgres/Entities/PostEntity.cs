using System.ComponentModel.DataAnnotations;
using Kruise.Domain;

namespace Kruise.DataAccess.Postgres.Entities;

public class PostEntity
{
    public PostEntity(long id, string title, long accountId)
    {
        Id = id;
        Title = title;
        AccountId = accountId;
    }

    public long Id { get; set; }

    [StringLength(PostModel.MaxTitleLength, ErrorMessage = Errors.Post.TitleMaxLength)]
    public string Title { get; set; }

    public long AccountId { get; set; }
}
