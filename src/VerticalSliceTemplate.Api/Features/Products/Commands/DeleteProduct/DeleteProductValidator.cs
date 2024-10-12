namespace VerticalSliceTemplate.Api.Features.Products.Commands.DeleteProduct;

internal sealed class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductValidator()
    {
        RuleFor(p => p.Id)
            .GreaterThan(0);
    }
}
