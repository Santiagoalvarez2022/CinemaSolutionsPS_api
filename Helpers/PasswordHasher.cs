using System.Security.Cryptography;

namespace CinemaSolutionApi.Helpers;

public static class PasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Interations = 100000;
    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;
    public static string Hash(string password)
    {
        var Salt = RandomNumberGenerator.GetBytes(SaltSize);
        var Hash = Rfc2898DeriveBytes.Pbkdf2(password, Salt, Interations, Algorithm, HashSize);

        return $"{Convert.ToHexString(Hash)}-{Convert.ToHexString(Salt)}";
    }

    public static bool Validate(string password, string hashedPassword)
    {
        var PartsPassword = hashedPassword.Split('-');
        var Hash = Convert.FromHexString(PartsPassword[0]);
        var Salt = Convert.FromHexString(PartsPassword[1]);
        var HashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, Salt, Interations, Algorithm, HashSize);

        return Hash.SequenceEqual(HashToCompare);
    }
}