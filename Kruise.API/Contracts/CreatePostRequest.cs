using System.ComponentModel.DataAnnotations;
using Kruise.Domain;

namespace Kruise.API.Contracts;

public class CreatePostRequest
{
    [Required(ErrorMessage = Errors.Post.TitleCanNotBeNullOrWhiteSpace)]
    [StringLength(Post.MaxTitleLength, ErrorMessage = Errors.Post.TitleMaxLength)]
    public string Title { get; set; }
}
