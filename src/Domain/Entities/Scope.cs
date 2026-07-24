namespace Domain.Entities;

public class Scope: BaseEntity
{
    public string Name { get; set; }
    
    public App App { get; set; }
}