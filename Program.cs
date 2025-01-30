using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MovieManagementApi.Core.Entities;
using MovieManagementApi.Core.Interfaces;
using MovieManagementApi.Core.Services;
using MovieManagementApi.Data;
using MovieManagementApi.Infrastructure.Repositories;
using MovieManagementApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Register services to the container (DI)

// 1. Register DbContext for EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Register repository services (Repository Pattern)
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<IMovieRatingRepository, MovieRatingRepository>();

// 3. Register the Application Service (Business Logic Layer)
builder.Services.AddScoped<MovieService>();
/*builder.Services.AddScoped<ActorService>();
builder.Services.AddScoped<MovieRatingService>();*/

// 4. Enable Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Movie Management API", Version = "v1" });
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Description = "Enter 'Bearer {your_api_key}' to access"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            new string[] {}
        }
    });
});

// 5. Add controllers (Web API)
builder.Services.AddControllers();

var app = builder.Build();

// Middleware pipeline setup

// Use Swagger for documentation (development environment only)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie Management API v1");
        c.RoutePrefix = string.Empty; // To serve Swagger UI at the root
    });
}

// Use HTTPS redirection
app.UseHttpsRedirection();

// Add authorization middleware (if needed for token validation)
app.UseAuthentication();  // For API secret validation
app.UseAuthorization();   // For roles and claims (if used)

// Configure routing and controllers
app.MapControllers();

// Run the application
app.Run();
