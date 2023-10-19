using Microsoft.EntityFrameworkCore;
using Number5Poc.Data.Entities;

namespace Number5Poc.Data;

public class Context: DbContext
{
    public DbSet<Permission> Permission { get; set; }
    public DbSet<PermissionType> PermissionType { get; set; }
    
    public Context (DbContextOptions<Context> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Permission>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<PermissionType>()
            .HasData(
                new List<PermissionType>{
                new PermissionType{ Id = 1, Description = "Maternidad"},
                new PermissionType{ Id = 2, Description = "Enfermedad"},
                new PermissionType{ Id = 3, Description = "Otros"}
                }
                );
    }
}