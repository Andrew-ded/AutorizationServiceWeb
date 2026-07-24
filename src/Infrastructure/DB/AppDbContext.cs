using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DB;

// dotnet ef migrations add Init --project src/Infrastructure --startup-project src/Web
// dotnet ef database update --project src/Infrastructure --startup-project src/Web
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ADUser> ADUsers => Set<ADUser>();
    
    public DbSet<Token> Tokens => Set<Token>();
    
    public DbSet<MobileUser> MobileUsers => Set<MobileUser>();

    public DbSet<Claim> Claims => Set<Claim>();

    public DbSet<Scope> Scopes => Set<Scope>();

    public DbSet<App> Apps => Set<App>();

    // public DbSet<ADUser> s => Set<ADUser>();
    //
    // public DbSet<ADUser> s => Set<ADUser>();
    // public DbSet<ADUser> s => Set<ADUser>();

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ADUser>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(200);
        });
    }
}