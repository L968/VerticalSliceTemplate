namespace VerticalSliceTemplate.Api.Infrastructure.Repositories.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
