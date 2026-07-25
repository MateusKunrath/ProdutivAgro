namespace ProdutivAgro.Application.Abstractions.Authentication;

public interface IPasswordEncrypter
{
    string Encrypt(string password);
    bool Verify(string password, string hashedPassword);
}