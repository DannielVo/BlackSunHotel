// Program.cs
using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


// Add services to the container.
builder.Services.AddControllers();
// Configure the DbContext with SQL Server (adjust the connection string as needed).
IConfigurationRoot cf = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();
builder.Services.AddDbContext<HotelSQL>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("cnn")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Redirect HTTP to HTTPS (if needed).
app.UseHttpsRedirection();

// Enable Swagger middleware for API documentation and testing.
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();
