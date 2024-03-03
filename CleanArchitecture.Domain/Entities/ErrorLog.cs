using CleanArchitecture.Domain.Abstract;

namespace CleanArchitecture.Domain.Entities;

public sealed class ErrorLog:Entity
{
    //UserId gibi bilgiler de eklenebilir.
    public string ErrorMessage { get; set; }
    public string StackTrace { get; set; }
    public string RequestPath { get; set; }
    public string RequestMethod { get; set; }
    public DateTime TimeStamp { get; set; }
}
