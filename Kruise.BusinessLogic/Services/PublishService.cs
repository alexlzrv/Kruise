using Kruise.Domain;
using Kruise.Domain.Interfaces;

namespace Kruise.BusinessLogic.Services;

public class PublishService : IPublishService
{
    private readonly IEnumerable<ISender> _senders;

    public PublishService(IEnumerable<ISender> senders)
    {
        _senders = senders;
    }

    public async Task SendPost(PostModel post)
    {
        foreach (var sender in _senders)
        {
            await sender.Send(post);
        }
    }
}
