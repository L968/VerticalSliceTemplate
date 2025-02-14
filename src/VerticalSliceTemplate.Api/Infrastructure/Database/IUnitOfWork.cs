namespace VerticalSliceTemplate.Api.Infrastructure.Database;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
