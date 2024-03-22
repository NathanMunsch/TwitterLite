using Microsoft.EntityFrameworkCore;
using TwitterLite.Server.Data;
using TwitterLite.Server.Helpers;
using TwitterLite.Server.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseLazyLoadingProxies();
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<TweetRepository>();
builder.Services.AddScoped<LikeRepository>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure Cross-Origin Resource Sharing (CORS)
app.UseCors(options => options
    .WithOrigins(new[] { "https://localhost:5173" })
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
);

// Enable HTTPS redirection for enhanced security
app.UseHttpsRedirection();

app.UseRouting();

// Enable authentication before authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<AuthMiddleware>();
app.UseMiddleware<UserAdminMiddleware>();

app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();
