using Scalar.AspNetCore;
using TestProject1.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddApiDocumentation();
builder.Services.AddControllers();
builder.Services.AddApplicationServices();
// Authentication and authorization
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

// Initialize database
await app.ApplyDatabaseMigrationsAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("My Application API")
            .WithTheme(ScalarTheme.Laserwave)
            .WithDefaultHttpClient(
                ScalarTarget.CSharp,
                ScalarClient.HttpClient);
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();