using Login.Application.Interfaces;
using Login.Domain.Entities;
using Login.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Login.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly LoginDbContext _context;

    public UserRepository(LoginDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email) => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User?> GetByIdAsync(Guid id) => await _context.Users.FindAsync(id);

    public async Task<bool> ExistsByEmailAsync(string email) => await _context.Users.AnyAsync(u => u.Email == email);

    public async Task AddAsync(User user) => await _context.Users.AddAsync(user);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}
