using Login.Domain.Entities;

namespace Login.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}
