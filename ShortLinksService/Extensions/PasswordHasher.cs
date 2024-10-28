using System.Security.Cryptography;
using System.Text;

namespace ShortLinksService.Extensions;

public class PasswordHasher
{

    public string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            return Convert.ToHexString(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }

    public bool VerifyHash(string dbhash, string password)
    {
        dbhash = HashPassword(dbhash);
        password = HashPassword(password);
        if (!dbhash.Equals(password))
        {
            throw new Exception();
        }

        return true;
    }
}