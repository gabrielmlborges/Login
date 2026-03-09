using Microsoft.EntityFrameworkCore;
using Login.Infrastructure.Data;
using Login.Application.Interfaces;
using Login.Application.Services;
using Login.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<LoginDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();

var allowedOrigins = builder.Configuration.GetSection("CorsSettings:AllowedOrigins").Get<String[]>();

builder.Services.AddCors(options => 
        {
        options.AddPolicy("MyPolicies", policy => {
                policy.WithOrigins(allowedOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod();
                });
        });

var app = builder.Build();

app.UseCors();
app.MapControllers();

// Aplicar migrations automaticamente no início
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<LoginDbContext>();
        if (context.Database.IsRelational())
        {
            context.Database.Migrate();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao rodar as migrations.");
    }
}

app.Run();
