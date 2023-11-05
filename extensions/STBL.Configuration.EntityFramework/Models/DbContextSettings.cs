using Microsoft.EntityFrameworkCore;
using STBL.Configuration.EntityFramework.Extensions;

namespace STBL.Configuration.EntityFramework.Models;

public class DbContextSettings<TDbContext>
    where TDbContext : DbContext
{
    public string? Host { get; set; }
    public int Port { get; set; }
    public string? Database { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is DbContextSettings<TDbContext> contextSettings)
        {
            return string.Equals(contextSettings.PropertiesToString<TDbContext>(), this.PropertiesToString());
        }

        return false;
    }

    public override int GetHashCode()
    {
        return this.PropertiesToString().GetHashCode();
    }
}