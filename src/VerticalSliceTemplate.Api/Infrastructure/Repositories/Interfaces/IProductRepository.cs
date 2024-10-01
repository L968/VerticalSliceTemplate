using VerticalSliceTemplate.Api.Domain;

namespace VerticalSliceTemplate.Api.Infrastructure.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);
    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Product Create(Product product);
    void Update(Product product);
    void Delete(Product product);
}
