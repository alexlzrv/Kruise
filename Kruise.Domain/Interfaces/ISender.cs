namespace Kruise.Domain.Interfaces;

public interface ISender
{
    Task Send(PostModel post);
}
