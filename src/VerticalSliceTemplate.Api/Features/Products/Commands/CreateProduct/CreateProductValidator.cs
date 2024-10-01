namespace VerticalSliceTemplate.Api.Features.Products.Commands.CreateProduct;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(p => p.Price)
            .GreaterThan(0);
    }
}
