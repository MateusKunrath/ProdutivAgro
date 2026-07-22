using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Token;
using FluentAssertions;
using ProdutivAgro.Application.UseCases.Users.Create;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace UseCases.Tests.Users.Create;

public class CreateUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestCreateUserJsonBuilder.Build();
        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
        result.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task ErrorNameEmpty()
    {
        var request = RequestCreateUserJsonBuilder.Build();
        request.Name = string.Empty;
        var useCase = CreateUseCase();

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.NAME_EMPTY));
    }

    [Fact]
    public async Task ErrorEmailAlreadyExists()
    {
        var request = RequestCreateUserJsonBuilder.Build();
        var useCase = CreateUseCase(request.Email);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex =>
            ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EMAIL_ALREADY_EXISTS));
    }

    private CreateUserUseCase CreateUseCase(string? email = null)
    {
        var readOnlyRepository = new UsersReadOnlyRepositoryBuilder();

        if (!string.IsNullOrWhiteSpace(email))
        {
            readOnlyRepository.ExistsActiveUserWithEmail(email);
        }

        return new CreateUserUseCase(
            readOnlyRepository.Build(),
            UsersWriteOnlyRepositoryBuilder.Build(),
            UnitOfWorkBuilder.Build(),
            JwtTokenGeneratorBuilder.Build(),
            new PasswordEncrypterBuilder().Build(),
            MapperBuilder.Build());
    }
}