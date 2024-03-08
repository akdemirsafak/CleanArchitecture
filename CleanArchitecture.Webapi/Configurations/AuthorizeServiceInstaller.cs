namespace CleanArchitecture.Webapi.Configurations;

public sealed class AuthorizeServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostBuilder hostBuilder)
    {
        services.AddAuthentication().AddJwtBearer(); //Burada araya girip yaptığımız ayarları JwtBearerOptionsSetup'da yapıyoruz.
        services.AddAuthorization();
    }
}
