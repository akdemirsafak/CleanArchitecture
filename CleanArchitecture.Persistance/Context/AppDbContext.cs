using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistance.Context;

public sealed class AppDbContext : DbContext
{
    //1. DbContext options yapısı ile program.cs'de dbcontext ayarı yapılır bu yöntemler ctor ile instance türetilerek çağırılır.
    //private readonly AppDbContext _dbContext; ctor'da dbContext i geç.
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    //2. bu class'ın new'lenerek çağırılmasını istersek aşağıda override onconfiguration ile yaparız.
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer("");
    //    base.OnConfiguring(optionsBuilder);
    //}
    //AppDbContext dbContext=new();


}
