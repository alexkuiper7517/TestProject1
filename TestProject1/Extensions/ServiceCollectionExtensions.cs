using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TestProject1.Data;
using TestProject1.Repositories;
using TestProject1.Service;
using TestProject1.Services;

namespace TestProject1.Extensions;

public static class ServiceCollectionExtensions
{
    private const string ConnectionString =
        "Data Source=TaskApp;Mode=Memory;Cache=Shared;Pooling=False";
    
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        // Repositories
        services.AddScoped<ITaskRepository, TaskRepository>();

        // Services
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<ITaskStatisticsService, TaskStatisticsService>();
        
        // Keep the in-memory database alive
        services.AddSingleton<SqliteConnection>(_ =>
        {
            var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            return connection;
        });

        // Register a scoped DbContext
        services.AddDbContext<AppDbContext>((provider, options) =>
        {
            // Ensure the keep-alive connection is opened
            _ = provider.GetRequiredService<SqliteConnection>();

            // Each DbContext manages its own connection
            options.UseSqlite(ConnectionString);
        });

        return services;
    }
}