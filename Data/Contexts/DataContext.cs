using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<UserEntity>(options)
{
    public virtual DbSet<ClientEntity> Clients { get; set; }
    public virtual DbSet<ProjectEntity> Projects { get; set; }
    
    public virtual DbSet<MemberAddressEntity> MemberAddresses { get; set; }
    
    
    //Chatgpt, To seed status. It is a hook provided by Entity Framework Core that allows
    // you to configure your models before they’re turned into database tables — during
    // the model creation phase.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}