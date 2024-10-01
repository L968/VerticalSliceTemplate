namespace VerticalSliceTemplate.Api.Features.Products.Queries.GetProductById;

public class GetProductByIdValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdValidator()
    {
        RuleFor(p => p.Id)
            .GreaterThan(0);
    }
}
