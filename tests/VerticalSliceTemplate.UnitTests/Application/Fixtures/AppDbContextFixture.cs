using Microsoft.EntityFrameworkCore;
using VerticalSliceTemplate.Application.Infrastructure.Database;

namespace VerticalSliceTemplate.UnitTests.Application.Fixtures;

public sealed class AppDbContextFixture : IDisposable
{
    private readonly AppDbContext _dbContext;

    internal AppDbContext DbContext
    {
        get
        {
            ResetDatabase();
            return _dbContext;
        }
    }

    public AppDbContextFixture()
    {
        DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _dbContext = new AppDbContext(options);
    }

    public void ResetDatabase()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.ChangeTracker.Clear();
    }

    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }
}

