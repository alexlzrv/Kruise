namespace Kruise.Domain.Models;

public class SenderModel
{
    public SenderModel(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }

    public string Name { get; set; }
}
