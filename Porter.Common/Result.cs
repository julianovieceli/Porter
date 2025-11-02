using System.Text.Json.Serialization;

namespace Porter.Common;

public class Result
{
    public Result() { }

    protected Result(string errorCode, string? errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        IsFailure = true;
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string ErrorCode { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ErrorMessage { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public bool IsFailure { get; init; }

    public static Result Success => new();
    public static Result Failure(string errorCode)
        => new(errorCode, null);

    public static Result Failure(string errorCode, string errorMessage)
        => new(errorCode, errorMessage);


}

public class Result<T> : Result
{
    public T Response { get; set; }

    protected Result(T response)
    {
        this.Response = response;
    }

    public static new Result<T> Success(T response) => new(response);
}


