using Kruise.Domain;
using Kruise.Domain.Interfaces;
using Kruise.Domain.Models;

namespace Kruise.BusinessLogic.Services;

public class PublishService : IPublishService
{
    public Task SendPost(PostModel post, SenderModel[] sender)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<SenderModel[]> GetSenders()
    {
        throw new NotImplementedException();
    }
}
