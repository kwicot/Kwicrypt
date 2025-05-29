using Kwicrypt.Module.Auth.Models;
using Kwicrypt.Module.Dto;

namespace Kwicrypt.Module.LoginSecrets.Interfaces;

public interface ILoginSecretsService
{
    public Task<bool> AddSecret(User user, LoginSecretDto dto);
    
    public Task<LoginSecretDto> GetSecretDto(Guid secretId);
    public Task<List<LoginSecretDto>> GetSecretsDto(User user);
    
    public Task<bool> UpdateSecretDto(LoginSecretDto dto);
    
    public Task<bool> DeleteSecretByUserId(Guid userId);
}