namespace VehicleManagement.Application.Exceptions;

public class BadRequestException(string message, Dictionary<string , string[]> errors) : Exception(message)
{
    public Dictionary<string, string[]> Errors = errors;
}
