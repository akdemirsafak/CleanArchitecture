using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistance.Context;
using FluentValidation;

namespace CleanArchitecture.Webapi.Middlewares;

public sealed class ExceptionMiddleware : IMiddleware
{
    private readonly AppDbContext _dbContext;

    public ExceptionMiddleware(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await LogExceptionToDatabase(context, ex);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        //Öncelikle hata tipini bulmalıyız. Eğer hata normal ise error result ile yollayacağız.Eğer validasyon hatasıysa exception validasyon hatasıyla throw edilir.
        //Validasyon hatası throw edeceğimizde hata mesajlarını array'e çevirip validation hatalarını bascağımız error result'a yollayacağız.

        context.Response.ContentType = "application/json"; //error result bir class eğer bunu json a çevirmezsek string'e çevirmeye çalışır veya hata atar.
        context.Response.StatusCode = 500;
        if (ex.GetType() == typeof(ValidationException))
        {

            return context.Response.WriteAsync(new ValidationErrorDetails
            {
                Errors = ((ValidationException)ex).Errors.Select(s => s.PropertyName),
                StatusCode = 400
            }.ToString());

        }
        return context.Response.WriteAsync(new ErrorResult
        {
            Message = ex.Message,
            StatusCode = context.Response.StatusCode
        }.ToString());
    }
    private async Task LogExceptionToDatabase(HttpContext context, Exception ex)
    {
        ErrorLog errorLog = new ErrorLog()
        {
            ErrorMessage = ex.Message,
            StackTrace=ex.StackTrace,
            RequestMethod = context.Request.Method,
            RequestPath = context.Request.Path,
            TimeStamp=DateTime.Now
        };
        await _dbContext.AddAsync(errorLog);
        await _dbContext.SaveChangesAsync();

    }
}
