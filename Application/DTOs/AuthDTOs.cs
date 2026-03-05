namespace Login.Application.DTOs;

public record RegisterDTO(string Email, string Password);
public record RegisterResponseDTO(Guid Id, string Email, string Role);
public record LoginDTO(string Email, string Password);
public record LoginResponseDTO(Guid Id, string Email, string Role, string Token);
