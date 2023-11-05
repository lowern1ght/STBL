using Npgsql;
using STBL.Configuration.EntityFramework.Extensions;
using STBL.Configuration.EntityFramework.Models;
using STBL.Configuration.Test.Models;
using Xunit.Abstractions;

namespace STBL.Configuration.Test;

public class DbContextSettingsExtensions
{
    private readonly ITestOutputHelper _outputHelper;

    public DbContextSettingsExtensions(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    public void ParserShouldTest()
    {
        var contextSettings = new DbContextSettings<TestDbContext>
        {
            Host = "localhost",
            Port = 5432,
            Database = "test-db",
            Username = "test-user",
            Password = "b0ef8467b6fc4a1eafaacc73abcf347a"
        };

        var stringBuilderTest = new NpgsqlConnectionStringBuilder
        {
            Host = contextSettings.Host,
            Port = contextSettings.Port,
            Database = contextSettings.Database,
            Username = contextSettings.Username,
            Password = contextSettings.Password
        };

        var stringBuilderResult = contextSettings.ParseToStringBuilder(new NpgsqlConnectionStringBuilder());

        _outputHelper.WriteLine($"TestBuilder: {stringBuilderTest.ConnectionString}" +
                                $"{Environment.NewLine}" +
                                $"ResultBuilder: {stringBuilderResult.ConnectionString}");

        Assert.Equal(stringBuilderTest.ConnectionString, stringBuilderResult.ConnectionString);
    }
}