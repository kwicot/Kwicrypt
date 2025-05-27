using Kwicrypt.Module.Auth.Models;

namespace Kwicrypt.Module.Auth.Interfaces;

public interface IRefreshTokenFactory
{
    public RefreshToken GetToken(Guid userId);
}