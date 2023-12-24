using Microsoft.EntityFrameworkCore;
using TwitterLite.Server.Data;
using TwitterLite.Server.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Services.AddDbContext<UserContext>(options =>
{
    options.UseSqlServer("Server=192.168.1.112;Initial Catalog=TwitterLite;User ID=nathan;Password=nathan;TrustServerCertificate=True;");
});
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<JwtService>();

builder.Services.AddControllers();


var app = builder.Build();

app.UseCors(options => options
    .WithOrigins(new[] { "https://localhost:5173/" })
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
);

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
