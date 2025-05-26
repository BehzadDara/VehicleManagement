namespace VehicleManagement.DomainService.Services;

public interface ITokenService
{
    public string Generate(string username, List<string> permissions);
}
