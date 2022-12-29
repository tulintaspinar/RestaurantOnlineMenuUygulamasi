using MenuWebAPI.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword}";

builder.Services.AddDbContext<MenuDbContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("MenuAPICors", opt =>
    {
        opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseCors("MenuAPICors");
// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
