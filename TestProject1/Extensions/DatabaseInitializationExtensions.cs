using Microsoft.EntityFrameworkCore;
using TestProject1.Data;

namespace TestProject1.Extensions;

public static class DatabaseInitializationExtensions
{
    public static async Task ApplyDatabaseMigrationsAsync(
        this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();

        var context = scope.ServiceProvider
            .GetRequiredService<AppDbContext>();

        await context.Database.MigrateAsync();
    }
}