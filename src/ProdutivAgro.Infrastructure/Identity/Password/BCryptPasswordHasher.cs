using ProdutivAgro.Application.Abstractions.Authentication;
using BC = BCrypt.Net.BCrypt;

namespace ProdutivAgro.Infrastructure.Identity.Password;

public class BCryptPasswordHasher : IPasswordEncrypter
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