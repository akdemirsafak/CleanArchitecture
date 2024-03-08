using CleanArchitecture.Application;
using CleanArchitecture.Application.Behaivors;
using FluentValidation;
using MediatR;

namespace CleanArchitecture.Webapi.Configurations;

public sealed class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostBuilder hostBuilder)
    {

        //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateCarCommand))); //Cqrs pattern Application katmanında kullanacağı için bu katmanın assembly referansı verildi.
        services.AddMediatR(cfr =>
            cfr.RegisterServicesFromAssembly(typeof(CleanArchitecture.Application.ApplicationAssemblyReference).Assembly));

        services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
        //services.AddValidatorsFromAssemblyContaining(typeof(ApplicationAssemblyReference));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

    }
}
