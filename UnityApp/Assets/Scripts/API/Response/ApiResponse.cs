public class ApiResponse
{
    public int StatusCode { get; }
    public string Body { get; }

    public ApiResponse(int statusCode, string body)
    {
        StatusCode = statusCode;
        Body = body;
    }
}
