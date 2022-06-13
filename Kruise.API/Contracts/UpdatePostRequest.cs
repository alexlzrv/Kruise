using System.ComponentModel.DataAnnotations;
using Kruise.Domain;

namespace Kruise.API.Contracts;

public class UpdatePostRequest
{
    public UpdatePostRequest(string title)
    {
        Title = title;
    }

    [Required(ErrorMessage = Errors.Post.TitleCanNotBeNullOrWhiteSpace)]
    [StringLength(PostModel.MaxTitleLength, ErrorMessage = Errors.Post.TitleMaxLength)]
    public string Title { get; set; }
}
