namespace VehicleManagement.API.Features;

public class BaseResult<T> : BaseResult
{
    public T? Value { get; set; }

    public void SetValue(T value)
    {
        Value = value;
    }
}

public class BaseResult
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public Dictionary<string, string[]> ValidationErrors { get; set; } = [];

    public static BaseResult Fail(string message, Dictionary<string, string[]> validationErrors)
    {
        var result = new BaseResult();

        result.Error(message, validationErrors);

        return result;
    }

    public static BaseResult Success(string message = "Done")
    {
        var result = new BaseResult();

        result.Ok(message);

        return result;
    }

    public static BaseResult<T> Success<T>(T value, string message = "Done")
    {
        var result = new BaseResult<T>();

        result.SetValue(value);
        result.Ok(message);

        return result;
    }

    private void Error(string message, Dictionary<string, string[]> validationErrors)
    {
        IsSuccess = false;
        Message = message;
        ValidationErrors = validationErrors;
    }

    private void Ok(string message)
    {
        IsSuccess = true;
        Message = message;
    }
}
