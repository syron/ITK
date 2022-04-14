using System.Text.Json.Serialization;

namespace KIT.Models.Services;

public class ServiceResponse<T>
{
    public ServiceResponse(T result)
    {
        Result = result;
    }

    public ServiceResponse(T result, Pagination pagination)
    {
        Result = result;
        Pagination = pagination;
    }

    public ServiceResponse(Error error) 
    {
        Error = error;
    }

    public T? Result { get; set; }
    
    public Pagination? Pagination { get; set; }
    
    public Error? Error { get; set; }
}