using System.Security.Cryptography;
using System.Linq;

namespace CinemaSolutionApi.Helpers;

public static class PasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Interations = 100000;

    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;
    
    public static  string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Interations, Algorithm, HashSize);
        
        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
    }

    public static bool Validate(string password, string hashedPassword)
    {
        var partsPassword = hashedPassword.Split('-');
        var hash = Convert.FromHexString(partsPassword[0]);
        var salt = Convert.FromHexString(partsPassword[1]);

        var HashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, Interations, Algorithm, HashSize);

        return hash.SequenceEqual(HashToCompare);
    }
}