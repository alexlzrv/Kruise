using Kruise.Domain.Models;

namespace Kruise.Domain.Interfaces;

public interface IPublishService
{
    Task SendPost(PostModel post);
}
