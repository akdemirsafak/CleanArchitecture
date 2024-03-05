using CleanArchitecture.Infrastructure.Options.Auth;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Webapi.OptionsSetup
{
    public sealed class JwtOptionsSetup : IConfigureOptions<JwtOptions>
    {
        private readonly IConfiguration _configuration;

        public JwtOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(JwtOptions options)
        {
            _configuration.GetSection("Jwt").Bind(options);
            throw new NotImplementedException();
        }
    }
}
