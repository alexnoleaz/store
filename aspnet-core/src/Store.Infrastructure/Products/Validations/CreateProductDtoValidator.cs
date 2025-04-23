using FluentValidation;
using Store.Products.Dtos;

namespace Store.Products.Validations;

public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(x => x.Sku).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.Status).IsInEnum();
    }
}
