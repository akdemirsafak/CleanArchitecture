using CleanArchitecture.Application;
using CleanArchitecture.Application.Behaivors;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Domain.UnitOfWorks;
using CleanArchitecture.Infrastructure.Options;
using CleanArchitecture.Infrastructure.Services;
using CleanArchitecture.Persistance.Context;
using CleanArchitecture.Persistance.Repositories;
using CleanArchitecture.Persistance.Services;
using CleanArchitecture.Persistance.UnitOfWorks;
using CleanArchitecture.Webapi.Middlewares;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);




string connectionString= builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));

//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateCarCommand))); //Cqrs pattern Application katmanında kullanacağı için bu katmanın assembly referansı verildi.


builder.Services.AddTransient<ExceptionMiddleware>();

builder.Services.AddMediatR(cfr =>
    cfr.RegisterServicesFromAssembly(typeof(CleanArchitecture.Application.ApplicationAssemblyReference).Assembly));

builder.Services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
//builder.Services.AddValidatorsFromAssemblyContaining(typeof(ApplicationAssemblyReference));

builder.Services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationBehavior<,>));

builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IMailService, MailService>();
var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);






builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));

builder.Services.AddAutoMapper(typeof(CleanArchitecture.Persistance.PersistanceAssemblyReference).Assembly);

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

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


//app.UseMiddleware<ErrorMiddleware>();

app.UseMiddlewareExtensions();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
