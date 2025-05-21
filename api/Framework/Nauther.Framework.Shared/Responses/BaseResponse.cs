namespace Nauther.Framework.Shared.Responses;

public class BaseResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<ValidationError>? ValidationErrors { get; set; }
    public object? Data { get; set; }
    public Dictionary<string, object> Metadata { get; set; } = [];
}

public class BaseResponse<T> : BaseResponse
{
    public new T? Data { get; set; }
}