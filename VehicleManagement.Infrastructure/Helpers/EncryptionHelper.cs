namespace VehicleManagement.Infrastructure.Helpers;

public static class EncryptionHelper
{
    public static string Encrypt(string str)
    {
        return $"{str}{str}";
    }

    public static string Decrypt(string str)
    {
        return str[..(str.Length / 2)];
    }
}
