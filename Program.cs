using Microsoft.EntityFrameworkCore;
using MovieManagementApi.Core.Interfaces;
using MovieManagementApi.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using MovieManagementApi.Core.Entities;
using MovieManagementApi.Core.Middleware;
using MovieManagementApi.Core.Services;
using MovieManagementApi.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")  // React frontend URL
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();  // Allow credentials if needed (e.g., cookies, Authorization headers)
    });
});

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=movies.db")); // You can replace this with any DB provider (SQL Server, SQLite, etc.)

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<ActorService>();

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
// Add configuration for API_SECRET_KEY
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();
app.UseCors("AllowReactApp");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie Management API v1");
        c.RoutePrefix = "/swagger"; // Makes Swagger UI available at root URL
    });
}

app.UseMiddleware<ApiKeyMiddleware>();

app.UseAuthorization();
app.MapControllers();

app.Run();