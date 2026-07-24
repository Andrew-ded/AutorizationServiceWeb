namespace Domain.Entities;

public class App: BaseEntity
{
    public Guid Gid { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}