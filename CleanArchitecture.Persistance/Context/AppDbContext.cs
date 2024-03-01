using CleanArchitecture.Domain.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistance.Context;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
 

    //Bu kısmı her entity için DbSet<Entity> şeklinde tanımlama yapmamak için kullandık.
    //IEntityTypeConfiguration'ın implemente edildiği tüm class'lar için geçerlidir.
    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistanceAssemblyReference).Assembly);
   
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
