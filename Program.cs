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

var app = builder.Build();

app.MapControllers();

app.Run();
