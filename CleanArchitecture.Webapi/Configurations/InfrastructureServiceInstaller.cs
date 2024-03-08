using CleanArchitecture.Application.Abstraction;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Infrastructure.Authentication;
using CleanArchitecture.Infrastructure.Options;
using CleanArchitecture.Infrastructure.Services;
using CleanArchitecture.Webapi.OptionsSetup;

namespace CleanArchitecture.Webapi.Configurations;

public sealed class InfrastructureServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostBuilder hostBuilder)
    {
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        services.AddScoped<IMailService, MailService>(); // Infrastructure katmanında.

        var emailConfig = configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
        services.AddSingleton(emailConfig);
    }
}
