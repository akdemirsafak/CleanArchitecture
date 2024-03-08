using CleanArchitecture.Webapi.Configurations;
using CleanArchitecture.Webapi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .InstallServices(builder.Configuration,
    builder.Host, 
    typeof(IServiceInstaller).Assembly);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

//app.UseMiddleware<ErrorMiddleware>();

app.UseMiddlewareExtensions();

app.UseHttpsRedirection();

//app.UseAuthorization(); Yukarıda builder.Services.AddAuthorization() yazmamız yeterli artık bu middleware i kullanmayabiliriz.

app.MapControllers();

app.Run();