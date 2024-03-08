using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Domain.UnitOfWorks;
using CleanArchitecture.Persistance.Repositories;
using CleanArchitecture.Persistance.Services;
using CleanArchitecture.Persistance.UnitOfWorks;
using CleanArchitecture.Webapi.Configurations;
using CleanArchitecture.Webapi.Middlewares;

namespace CleanArchitecture.Webapi;

public sealed class PersistanceDIServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostBuilder hostBuilder)
    {
        services.AddTransient<ExceptionMiddleware>();
        services.AddScoped<ICarService, CarService>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    }
}
