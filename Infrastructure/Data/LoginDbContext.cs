using Microsoft.EntityFrameworkCore;
using Login.Domain.Entities;

namespace Login.Infrastructure.Data;

public class LoginDbContext : DbContext
{
    public LoginDbContext(DbContextOptions<LoginDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
}
