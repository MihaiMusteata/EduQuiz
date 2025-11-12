namespace SharedLibrary.Wrapper;

public abstract class ApiResponseBase
{
    public bool Success { get; set; }
    public string? Message { get; set; }
}

public class ApiResponse : ApiResponseBase
{
    public static ApiResponse Ok(string? message = null) =>
        new ApiResponse { Success = true, Message = message };

    public static ApiResponse Fail(string message) =>
        new ApiResponse { Success = false, Message = message };
}

public class ApiResponse<T> : ApiResponseBase
{
    public T? Data { get; set; }

    public static ApiResponse<T> Ok(T data, string? message = null) =>
        new ApiResponse<T> { Success = true, Data = data, Message = message };

    public static ApiResponse<T> Fail(string message) =>
        new ApiResponse<T> { Success = false, Message = message };
}