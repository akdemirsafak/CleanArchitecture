using CleanArchitecture.Domain.Abstract;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistance.Context;

public sealed class AppDbContext : IdentityDbContext<AppUser,AppRole, string>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
 

    //Bu kısmı her entity için DbSet<Entity> şeklinde tanımlama yapmamak için kullandık.
    //IEntityTypeConfiguration'ın implemente edildiği tüm class'lar için geçerlidir.
    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    { 
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistanceAssemblyReference).Assembly);
        base.OnModelCreating(modelBuilder);
        
        //Yukarıdakinin çalışmadığı durumda aşağıdaki yapılabilir.

        //base.OnModelCreating(modelBuilder);
        //modelBuilder.Ignore<IdentityUserLogin<string>>();
        //modelBuilder.Ignore<IdentityUserRole<string>>();
        //modelBuilder.Ignore<IdentityUserClaim<string>>();
        //modelBuilder.Ignore<IdentityUserToken<string>>();
        //modelBuilder.Ignore<IdentityUser<string>>();
        //modelBuilder.Ignore<AppUser>();

    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries= ChangeTracker.Entries<Entity>();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(p => p.CreatedAt).CurrentValue = DateTime.UtcNow;
            }
            if (entry.State == EntityState.Modified)
            {
                entry.Property(p => p.UpdatedAt).CurrentValue = DateTime.UtcNow;
            }

        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
