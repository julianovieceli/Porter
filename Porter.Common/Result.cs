using System.Net;
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
    public T Value { get; set; }

    protected Result(T value)
    {
        this.Value = value;
    }

    public static Result<T> Success(T value) => new(value);
}


