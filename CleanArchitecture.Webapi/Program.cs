using CleanArchitecture.Application;
using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Persistance.Context;
using CleanArchitecture.Persistance.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);




string connectionString= builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));

//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateCarCommand))); //Cqrs pattern Application katmanında kullanacağı için bu katmanın assembly referansı verildi.


builder.Services.AddMediatR(cfr =>
    cfr.RegisterServicesFromAssembly(typeof(CleanArchitecture.Application.ApplicationAssemblyReference).Assembly));

builder.Services.AddScoped<ICarService, CarService>();

builder.Services.AddAutoMapper(typeof(CleanArchitecture.Persistance.PersistanceAssemblyReference).Assembly);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(CleanArchitecture.Presentation.PresentationAssemblyReference).Assembly); //Mevcut uygulamada başka bir katmanda controller'ların olduğunu bildirdik.



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
