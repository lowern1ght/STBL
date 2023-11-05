using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using STBL.Configuration.EntityFramework.Extensions;
using STBL.Configuration.EntityFramework.Models;
using STBL.Configuration.Test.Models;

namespace STBL.Configuration.Test;

public class ConfigurationExtensions
{
    [Fact]
    public void ParseConfigToDbContext()
    {
        var configurationBuilder = new ConfigurationBuilder();

        var configuration = configurationBuilder
            .AddDbContextSettingsFile<TestDbContext>()
            .Build();

        var contextSettingsResult = configuration.GetDbContextSettings<TestDbContext>();

        var path = Path.Combine(Environment.CurrentDirectory, $"{nameof(TestDbContext).ToLower()}.json");

        Assert.Equal(ParseJsonToContextSettings(path), contextSettingsResult);
    }

    private static DbContextSettings<TestDbContext> ParseJsonToContextSettings(string path)
    {
        dynamic deserialize = JsonConvert.DeserializeObject(File.ReadAllText(path)) ?? throw new InvalidOperationException();

        dynamic dynamicContextSettings = deserialize.TestDbContext;

        return new DbContextSettings<TestDbContext>
        {
            Host = dynamicContextSettings.Host,
            Port = dynamicContextSettings.Port,
            Database = dynamicContextSettings.Database,
            Username = dynamicContextSettings.Username,
            Password = dynamicContextSettings.Password
        };
    }
}