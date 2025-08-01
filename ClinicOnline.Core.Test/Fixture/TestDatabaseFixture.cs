using ClinicOnline.Infrastructure.DatabaseContexts;
using ClinicOnline.Infrastructure.Test.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ClinicOnline.Core.Test.Fixture;

public class TestDatabaseFixture
{
    public delegate Task AsyncTask<in T>(T obj);
    private static readonly object _lock = new();
    private static bool _databaseInitialized;

    public TestDatabaseFixture()
    {
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using (var context = CreateContext())
                {
                    context.Database.EnsureDeleted();
                    context.Database.Migrate();
                }

                _databaseInitialized = true;
            }
        }
    }

    private static ILoggerFactory CreateLoggerFactory()
    {
        return new LoggerFactory().AddSerilog(new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("Log/log.txt")
            .CreateLogger());
    }

    private ClinicManagementContext CreateContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ClinicManagementContext>()
            .UseNpgsql(Infrastructure.Test.Const.ConnectionString,
                x => x.MigrationsHistoryTable("__MyMigrationsHistory", "bukken"))
            .UseLoggerFactory(CreateLoggerFactory())
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        return new ClinicManagementContext(optionsBuilder.Options);
    }

    public async Task ExecuteTest(AsyncTask<ApplicationSeeder> seed, AsyncTask<ClinicManagementContext> test)
    {

        var context = CreateContext();
        try
        {
            await context.Database.BeginTransactionAsync();
            await seed(new ApplicationSeeder(context));
            await context.SaveChangesAsync();
            await test(context);
        }
        finally
        {
            await context.Database.RollbackTransactionAsync();
        }
    }
}
