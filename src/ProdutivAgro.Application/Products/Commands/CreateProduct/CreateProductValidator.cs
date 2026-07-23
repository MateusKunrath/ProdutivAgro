using FluentValidation;

namespace ProdutivAgro.Application.Products.Commands.CreateProduct;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.UnitPrice).GreaterThan(0);
    }
}