using FluentAssertions;
using MediatR;
using Moq;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Abstractions.Persistence;
using ProdutivAgro.Application.Sales.Commands.AddSaleItems;
using ProdutivAgro.Domain.Products.Entities;
using ProdutivAgro.Domain.Products.Enums;
using ProdutivAgro.Domain.Products.Repositories;
using ProdutivAgro.Domain.Sales.Entities;
using ProdutivAgro.Domain.Sales.Repositories;

namespace ProdutivAgro.Application.UnitTests.Sales.Commands;

public class AddSaleItemsCommandHandlerTests
{
    [Fact]
    public async Task SuccessAddsNewItemsAsNewEntities()
    {
        var organizationId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var product = new Product(organizationId, "Tomato", 12.50m, MeasurementUnit.Kilogram);
        var sale = new Sale(organizationId, userId, DateTimeOffset.UtcNow);

        var salesUpdateOnlyRepository = new Mock<ISalesUpdateOnlyRepository>();
        salesUpdateOnlyRepository
            .Setup(repository => repository.GetByIdAsync(sale.Id, organizationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(sale);

        var salesWriteOnlyRepository = new Mock<ISalesWriteOnlyRepository>();
        var productsReadOnlyRepository = new Mock<IProductsReadOnlyRepository>();
        productsReadOnlyRepository
            .Setup(repository => repository.GetByIdsAsync(
                It.IsAny<IEnumerable<Guid>>(),
                organizationId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([product]);

        var currentUser = new Mock<ICurrentUser>();
        currentUser.SetupGet(user => user.OrganizationId).Returns(organizationId);

        var unitOfWork = new Mock<IUnitOfWork>();
        var handler = new AddSaleItemsCommandHandler(
            salesUpdateOnlyRepository.Object,
            salesWriteOnlyRepository.Object,
            productsReadOnlyRepository.Object,
            currentUser.Object,
            unitOfWork.Object);
        var command = new AddSaleItemsCommand
        {
            SaleId = sale.Id,
            Items = [new AddSaleItemCommand { ProductId = product.Id, Quantity = 2 }],
        };

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().ContainSingle(item =>
            item.ProductId == product.Id &&
            item.SaleItemId != Guid.Empty);

        salesWriteOnlyRepository.Verify(repository => repository.AddItemsAsync(
            It.Is<IEnumerable<SaleItem>>(items => items.Single().SaleId == sale.Id),
            It.IsAny<CancellationToken>()), Times.Once);
        unitOfWork.Verify(work => work.Commit(), Times.Once);
    }

    [Fact]
    public async Task SuccessAddsRepeatedProductAsSeparateSaleItems()
    {
        var organizationId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var product = new Product(organizationId, "Tomato", 12.50m, MeasurementUnit.Kilogram);
        var sale = new Sale(organizationId, userId, DateTimeOffset.UtcNow);

        var salesUpdateOnlyRepository = new Mock<ISalesUpdateOnlyRepository>();
        salesUpdateOnlyRepository
            .Setup(repository => repository.GetByIdAsync(sale.Id, organizationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(sale);

        var salesWriteOnlyRepository = new Mock<ISalesWriteOnlyRepository>();
        var productsReadOnlyRepository = new Mock<IProductsReadOnlyRepository>();
        productsReadOnlyRepository
            .Setup(repository => repository.GetByIdsAsync(
                It.IsAny<IEnumerable<Guid>>(),
                organizationId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([product]);

        var currentUser = new Mock<ICurrentUser>();
        currentUser.SetupGet(user => user.OrganizationId).Returns(organizationId);

        var handler = new AddSaleItemsCommandHandler(
            salesUpdateOnlyRepository.Object,
            salesWriteOnlyRepository.Object,
            productsReadOnlyRepository.Object,
            currentUser.Object,
            Mock.Of<IUnitOfWork>());

        var result = await handler.Handle(new AddSaleItemsCommand
        {
            SaleId = sale.Id,
            Items =
            [
                new AddSaleItemCommand { ProductId = product.Id, Quantity = 2 },
                new AddSaleItemCommand { ProductId = product.Id, Quantity = 3 },
            ],
        }, CancellationToken.None);

        result.Should().HaveCount(2);
        result.Should().OnlyContain(item => item.ProductId == product.Id);
        result.Select(item => item.SaleItemId).Should().OnlyHaveUniqueItems();
        sale.Items.Should().HaveCount(2);
    }
}
