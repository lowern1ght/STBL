using System.Data.Common;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using STBL.Configuration.EntityFramework.Models;

namespace STBL.Configuration.EntityFramework.Extensions;

public static class DbContextSettingsExtensions
{
    public static DbConnectionStringBuilder ParseToStringBuilder<TDbContext>(
        this DbContextSettings<TDbContext> contextSettings,
        DbConnectionStringBuilder? stringBuilder = null)
        where TDbContext : DbContext
    {
        stringBuilder ??= new DbConnectionStringBuilder();

        foreach (var property in contextSettings.GetType().GetProperties())
        {
            stringBuilder[property.Name] = property.GetValue(contextSettings);
        }

        return stringBuilder;
    }

    public static string PropertiesToString<TDbContext>(this DbContextSettings<TDbContext> contextSettings)
        where TDbContext : DbContext
    {
        var stringBuilder = new StringBuilder();

        foreach (var property in contextSettings.GetType().GetProperties())
        {
            stringBuilder.Append(property.GetValue(contextSettings));
        }

        return stringBuilder.ToString();
    }
}