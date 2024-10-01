namespace VerticalSliceTemplate.Api.Features.Products.Commands.UpdateProduct;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(p => p.Price)
            .GreaterThan(0);
    }
}
