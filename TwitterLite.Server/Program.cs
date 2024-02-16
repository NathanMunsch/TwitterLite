using Microsoft.EntityFrameworkCore;
using TwitterLite.Server.Data;
using TwitterLite.Server.Helpers;
using TwitterLite.Server.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<UserRepository>();


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

// Use a MapWhen branch to conditionally apply middleware based on the request path
app.MapWhen(context => !context.Request.Path.StartsWithSegments("/auth/register") && !context.Request.Path.StartsWithSegments("/auth/login"), branch =>
{
    branch.UseMiddleware<AuthMiddleware>();

    // Map controllers and fallback to index.html if no other endpoint is matched
    branch.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapFallbackToFile("/index.html");
    });
});

app.Run();

