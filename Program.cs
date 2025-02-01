using Microsoft.EntityFrameworkCore;
using MovieManagementApi.Core.Interfaces;
using MovieManagementApi.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using MovieManagementApi.Core.Services;
using MovieManagementApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=movies.db")); // You can replace this with any DB provider (SQL Server, SQLite, etc.)

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<MovieService>();

// Add Swagger services
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Movie Management API",
        Version = "v1",
        Description = "A simple API to manage movies, actors, and ratings"
    });

    // If you want to include XML comments in your Swagger UI (make sure to enable XML comments in your .csproj)
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "MovieManagementApi.xml"));
    options.IgnoreObsoleteActions();
    options.IgnoreObsoleteProperties();
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Enable handling of circular references
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie Management API v1");
        c.RoutePrefix = string.Empty; // Makes Swagger UI available at root URL
    });
}

app.UseAuthorization();
app.MapControllers();

app.Run();