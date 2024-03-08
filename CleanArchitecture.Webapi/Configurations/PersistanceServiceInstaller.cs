using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CleanArchitecture.Webapi.Configurations;

public sealed class PersistanceServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostBuilder hostBuilder)
    {

        services.AddAutoMapper(typeof(CleanArchitecture.Persistance.PersistanceAssemblyReference).Assembly);

        string connectionString= configuration.GetConnectionString("DefaultConnection")!;
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));

        services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<AppDbContext>();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information() //Verbose en alt leveldir.
            .Enrich.FromLogContext() //Context'den gelen değerlerden daha detaylı log yapmamızı sağlar.
            .WriteTo.Console()
            .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)//Hergün yeni bir dosyaya kaydedilir.
            .WriteTo.MSSqlServer(connectionString,tableName:"Logs",autoCreateSqlTable:true)
            .CreateLogger();
        hostBuilder.UseSerilog();
    }
}
