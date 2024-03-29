﻿using System.ComponentModel.DataAnnotations;
using Kruise.Domain;

namespace Kruise.API.Contracts;

public class CreatePostRequest
{
    public CreatePostRequest(string title)
    {
        Title = title;
    }

    [Required(ErrorMessage = Errors.Post.TitleCanNotBeNullOrWhiteSpace)]
    [StringLength(PostModel.MaxTitleLength, ErrorMessage = Errors.Post.TitleMaxLength)]
    public string Title { get; set; }
}
