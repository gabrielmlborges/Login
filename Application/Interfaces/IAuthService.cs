using Login.Application.DTOs;

namespace Login.Application.Interfaces;

public interface IAuthService
{
    Task<RegisterResponseDTO?> RegisterAsync(RegisterDTO request);
    Task<LoginResponseDTO?> LoginAsync(LoginDTO request);
    Task<bool> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword);
}
