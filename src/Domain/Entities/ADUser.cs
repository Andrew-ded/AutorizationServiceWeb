namespace Domain.Entities;

public class ADUser : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    
    public string FIO { get; set; } = string.Empty;
   
}