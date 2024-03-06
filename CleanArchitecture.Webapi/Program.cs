using CleanArchitecture.Application;
using CleanArchitecture.Application.Abstraction;
using CleanArchitecture.Application.Behaivors;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Domain.UnitOfWorks;
using CleanArchitecture.Infrastructure.Authentication;
using CleanArchitecture.Infrastructure.Options;
using CleanArchitecture.Infrastructure.Services;
using CleanArchitecture.Persistance.Context;
using CleanArchitecture.Persistance.Repositories;
using CleanArchitecture.Persistance.Services;
using CleanArchitecture.Persistance.UnitOfWorks;
using CleanArchitecture.Webapi.Middlewares;
using CleanArchitecture.Webapi.OptionsSetup;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
builder.Services.AddScoped<IJwtProvider, JwtProvider>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IMailService, MailService>();

builder.Services.AddHttpContextAccessor();

var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();


builder.Services.AddAuthentication().AddJwtBearer(); //Burada araya girip yaptığımız ayarları JwtBearerOptionsSetup'da yapıyoruz.
builder.Services.AddAuthorization();

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));

builder.Services.AddAutoMapper(typeof(CleanArchitecture.Persistance.PersistanceAssemblyReference).Assembly);

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(CleanArchitecture.Presentation.PresentationAssemblyReference).Assembly); 
//Mevcut uygulamada başka bir katmanda controller'ların olduğunu bildirdik.


builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecuritySheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecuritySheme, Array.Empty<string>() }
                });
});


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseMiddleware<ErrorMiddleware>();

app.UseMiddlewareExtensions();

app.UseHttpsRedirection();

//app.UseAuthorization(); Yukarıda builder.Services.AddAuthorization() yazmamız yeterli artık bu middleware i kullanmayabiliriz.

app.MapControllers();

app.Run();
