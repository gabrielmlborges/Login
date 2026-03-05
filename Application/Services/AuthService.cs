using Login.Application.DTOs;
using Login.Application.Interfaces;
using Login.Domain.Entities;
using Login.Domain.Enums;

namespace Login.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public AuthService(IUserRepository userRepository, ITokenService tokenSerivce)
    {
        _userRepository = userRepository;
        _tokenService = tokenSerivce;
    }

    public async Task<RegisterResponseDTO?> RegisterAsync(RegisterDTO request)
    {
        if (await _userRepository.ExistsByEmailAsync(request.Email)) return null;

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = UserRole.Normal
        };

        await _userRepository.AddAsync(user);

        await _userRepository.SaveChangesAsync();

        return new RegisterResponseDTO(user.Id, user.Email, user.Role.ToString());
    }

    public async Task<LoginResponseDTO?> LoginAsync(LoginDTO request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash)) return null;

        var token = _tokenService.GenerateToken(user);

        return new LoginResponseDTO(user.Id, user.Email, user.Role.ToString(), token);
    }

    public async Task<bool> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null || !BCrypt.Net.BCrypt.Verify(oldPassword, user?.PasswordHash)) return false;

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

        await _userRepository.SaveChangesAsync();

        return true;
    }
}
