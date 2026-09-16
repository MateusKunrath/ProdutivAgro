using Moq;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Domain.Identity.Entities;

namespace ProdutivAgro.Testing.Common.Token;

public class JwtTokenGeneratorBuilder
{
    public static IJwtTokenGenerator Build()
    {
        var mock = new Mock<IJwtTokenGenerator>();
        mock.Setup(jwtTokenGenerator => jwtTokenGenerator.Generate(It.IsAny<User>())).Returns(
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWUsImlhdCI6MTUxNjIzOTAyMn0.KMUFsIDTnFmyG3nMiGM6H9FNFUROf3wh7SmqJp-QV30");
        return mock.Object;
    }
}