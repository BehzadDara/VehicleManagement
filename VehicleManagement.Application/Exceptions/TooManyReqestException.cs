namespace VehicleManagement.Application.Exceptions;

public class TooManyReqestException(string message) : Exception(message)
{
}

