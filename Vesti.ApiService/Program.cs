using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vesti.ApiService.Data;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AuthDBContextConnection") ?? throw new InvalidOperationException("Connection string 'AuthDBContextConnection' not found.");

builder.Services.AddDbContext<AuthDBContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddDefaultIdentity<VestiUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AuthDBContext>();

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.MapDefaultEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
