using Microsoft.EntityFrameworkCore;
using PicPay.Infrastructure.Persistence;
using Testcontainers.PostgreSql;

namespace PicPay.Tests.Integration;

public sealed class PostgreSqlFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder("postgres:16-alpine")
        .WithDatabase("picpay_tests")
        .WithUsername("test")
        .WithPassword("test")
        .Build();

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
        await ResetDatabaseAsync();
    }

    public async Task DisposeAsync() => await _container.DisposeAsync();

    public DbConnection CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<DbConnection>()
            .UseNpgsql(_container.GetConnectionString())
            .Options;

        return new DbConnection(options);
    }

    public async Task ResetDatabaseAsync()
    {
        await using var context = CreateDbContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.MigrateAsync();
    }
}

[CollectionDefinition(Name)]
public sealed class PostgreSqlCollection : ICollectionFixture<PostgreSqlFixture>
{
    public const string Name = "PostgreSQL integration tests";
}
