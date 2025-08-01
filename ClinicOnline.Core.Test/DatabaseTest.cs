using ClinicOnline.Infrastructure.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using Respawn;
using Respawn.Graph;
using Serilog;
using Xunit;

namespace ClinicOnline.Core.Test;

[Collection("DatabaseTest")]
public class DatabaseTest
{
    protected delegate Task Seeding<in T>(T obj);

    protected readonly ClinicManagementContext Context;

    private readonly Dictionary<string, string> section = new Dictionary<string, string>();

    public static Respawner DbRespawner;

    private static bool _databaseInitialized;

    protected DatabaseTest ()
    {
        section.Add("ClinicFolderName", "share-jnet");
        Context = CreateDbContext();
    }

    private ClinicManagementContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ClinicManagementContext>()
            .UseNpgsql(Infrastructure.Test.Const.ConnectionString,
                x => x.MigrationsHistoryTable("__MyMigrationsHistory", "bukken"))
            .UseLoggerFactory(CreateLoggerFactory())
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        return new ClinicManagementContext(optionsBuilder.Options);
    }

    private static ILoggerFactory CreateLoggerFactory()
    {
        return new LoggerFactory().AddSerilog(new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("Log/log.txt")
            .CreateLogger());
    }
    protected async Task Seed(
        Seeding<Infrastructure.Test.Seeds.ApplicationSeeder>? seed = null
    )
    {
        try
        {
            if (!_databaseInitialized)
            {
                _databaseInitialized = true;
                await Context.Database.EnsureDeletedAsync();
                await Context.Database.MigrateAsync();
                await using var conn = new NpgsqlConnection(Infrastructure.Test.Const.ConnectionString);
                await conn.OpenAsync();
                DbRespawner = await Respawner.CreateAsync(
                    conn,
                    new RespawnerOptions
                    {
                        TablesToIgnore = new Table[] {
                            "__MyMigrationsHistory",
                            "DataProtectionKeys",
                        },
                        SchemasToInclude = new[] {
                            "bukken",
                            "public"
                        },
                        DbAdapter = DbAdapter.Postgres
                    }
                );
            }
            else
            {
                await using var conn = new NpgsqlConnection(Infrastructure.Test.Const.ConnectionString);
                await conn.OpenAsync();
                if (seed is not null)
                {
                    await DbRespawner.ResetAsync(conn);
                }
            }
            
            if(seed is not null)
            {
                await seed(new Infrastructure.Test.Seeds.ApplicationSeeder(Context));
                await Context.SaveChangesAsync();
            }
        } catch( Exception ex )
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}
