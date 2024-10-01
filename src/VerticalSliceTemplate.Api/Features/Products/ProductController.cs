using VerticalSliceTemplate.Api.Domain;
using VerticalSliceTemplate.Api.Features.Products.Commands.CreateProduct;
using VerticalSliceTemplate.Api.Features.Products.Commands.DeleteProduct;
using VerticalSliceTemplate.Api.Features.Products.Commands.UpdateProduct;
using VerticalSliceTemplate.Api.Features.Products.Queries.GetProductById;
using VerticalSliceTemplate.Api.Features.Products.Queries.GetProducts;

namespace VerticalSliceTemplate.Api.Features.Products;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> Get()
    {
        var query = new GetProductsQuery();
        var response = await _mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetProductByIdQuery { Id = id };
        var response = await _mediator.Send(query);

        if (response is null) return NotFound();

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var response = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteProductCommand { Id = id });
        return NoContent();
    }
}
