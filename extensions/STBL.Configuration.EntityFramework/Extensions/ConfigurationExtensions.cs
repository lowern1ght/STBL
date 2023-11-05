using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using STBL.Configuration.EntityFramework.Models;

namespace STBL.Configuration.EntityFramework.Extensions;

public static class ConfigurationExtensions
{
    #region ConfigurationBuilder

    public static IConfigurationBuilder AddDbContextSettingsFile<TDbContext>(
        this IConfigurationBuilder configurationBuilder,
        string? pathToConfig = null)
            where TDbContext : DbContext
    {
        if (pathToConfig is null)
        {
            var nameContext = typeof(TDbContext).Name.ToLower();
            pathToConfig = Path.Combine(Environment.CurrentDirectory, $"{nameContext}.json");
        }

        return configurationBuilder.AddJsonFile(pathToConfig, true, true);
    }

    #endregion

    #region ServiceCollection

    public static IServiceCollection AddDbContextSettings<TDbContext>(this IServiceCollection serviceCollection)
        where TDbContext : DbContext
    {
        var configuration = serviceCollection
            .BuildServiceProvider()
            .GetRequiredService<IConfiguration>();

        var contextSettings = configuration.GetDbContextSettings<TDbContext>();

        return serviceCollection.AddSingleton(contextSettings);
    }

    #endregion

    #region Configuration

    private static readonly string ContextSection = "DatabaseConnections";

    public static DbContextSettings<TDbContext> GetDbContextSettings<TDbContext>(this IConfiguration configuration)
        where TDbContext : DbContext
    {
        var typeName = typeof(TDbContext).Name;

        var contextSettings = configuration.GetSection(ContextSection).GetSection(typeName).Get<DbContextSettings<TDbContext>>() ??
                              configuration.GetSection(typeName).Get<DbContextSettings<TDbContext>>();

        if (contextSettings is null)
        {
            throw new InvalidOperationException($"Error parse {typeName} from configuration");
        }

        return contextSettings;
    }

    public static string ConnectionStringFromDbSettings<TDbContext>(this IConfiguration configuration)
        where TDbContext : DbContext
    {
        return configuration
            .GetDbContextSettings<TDbContext>()
            .ParseToStringBuilder()
            .ConnectionString;
    }

    #endregion
}