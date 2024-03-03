using Newtonsoft.Json;

namespace CleanArchitecture.Webapi.Middlewares;

public sealed class ErrorResult : ErrorStatusCode
{
    public string Message { get; set; }
}

public sealed class ValidationErrorDetails : ErrorStatusCode
{
    public IEnumerable<string> Errors { get; set; }
}
public class ErrorStatusCode
{
    public int StatusCode { get; set; }
    public override string ToString()
    {
        /*return JsonSerializer.Serialize(this); */
        return JsonConvert.SerializeObject(this);
    }
}