namespace Domain.Entities;

public class Token: BaseEntity
{
    public string Subject { get; set; }
    
    public int ExpiresIn { get; set; } 
    
    public List<Claim> Claims { get; set; }
    
    public List<Scope> Scopes { get; set; }
    
    public App App {get; set;}
    
    public bool IsActive { get; set; } = true;
}