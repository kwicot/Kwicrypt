using Backend.Modules.Auth.Models;

namespace Backend.Modules.Auth.Interfaces;

public interface IRefreshTokenFactory
{
    public RefreshToken GetToken(Guid userId);
}