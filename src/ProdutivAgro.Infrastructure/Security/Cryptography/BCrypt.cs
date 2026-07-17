using ProdutivAgro.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace ProdutivAgro.Infrastructure.Security.Cryptography;

public class BCrypt : IPasswordEncrypter
{
    public string Encrypt(string password)
    {
        var hashedPassword = BC.HashPassword(password);
        return hashedPassword;
    }

    public bool Verify(string password, string hashedPassword)
    {
        return BC.Verify(password, hashedPassword);
    }
}