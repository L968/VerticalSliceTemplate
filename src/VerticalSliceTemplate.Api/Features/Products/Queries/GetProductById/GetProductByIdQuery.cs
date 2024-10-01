namespace VerticalSliceTemplate.Api.Features.Products.Queries.GetProductById;

public class GetProductByIdQuery : IRequest<GetProductByIdResponse>
{
    public int Id { get; set; }
}
