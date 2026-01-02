namespace GoldenEurope.API.Models;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? ErrorMessage { get; set; }
    public int StatusCode { get; set; }

    public static ApiResponse<T> SuccessResult(T? data, int statusCode = 200)
        => new() { Success = true, Data = data, StatusCode = statusCode };   
    public static ApiResponse<T> ErrorResult(string error, int statusCode = 400)
        => new() { Success = false, ErrorMessage = error, StatusCode = statusCode };   
}