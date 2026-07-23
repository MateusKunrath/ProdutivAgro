namespace ProdutivAgro.Domain.Identity.Services;

public interface IPasswordEncrypter
{
    string Encrypt(string password);
    bool Verify(string password, string hashedPassword);
}