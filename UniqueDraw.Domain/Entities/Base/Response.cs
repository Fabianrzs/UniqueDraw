namespace UniqueDraw.Domain.Entities.Base;

public class Response<T>(string message, T data, bool success) : Response(message, success)
{
    public new string Message { get; set; } = message;
    public new bool Success { get; set; } = success;
    public T Data { get; set; } = data;
}

public class Response(string message, bool success)
{
    public string Message { get; set; } = message;
    public bool Success { get; set; } = success;
}
