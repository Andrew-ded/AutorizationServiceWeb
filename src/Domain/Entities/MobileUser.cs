namespace Domain.Entities;

public class MobileUser: BaseEntity
{
    public required string Username { get; set; } = string.Empty;

    public required string Code { get; set; }
    
    public required int ADUserId { get; set; }
    public  ADUser ADUser { get; set; }
    
    
}