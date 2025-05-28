using Kwicrypt.Module.Auth.Models;
using Kwicrypt.Module.LoginSecrets.DTOs;
using Kwicrypt.Module.LoginSecrets.Interfaces;

namespace Kwicrypt.Module.LoginSecrets.Services;

public class LoginSecretsService : ILoginSecretsService
{
    private readonly ILoginSecretFactory _loginSecretFactory;
    private readonly ILoginSecretRepository _loginSecretRepository;

    public LoginSecretsService(
        ILoginSecretFactory loginSecretFactory,
        ILoginSecretRepository loginSecretRepository)
    {
        _loginSecretFactory = loginSecretFactory;
        _loginSecretRepository = loginSecretRepository;
    }
    
    public async Task<bool> AddSecret(User user, LoginSecretDto dto)
    {
        var loginSecret = await _loginSecretFactory.GetLoginDataAsync(
            user.Id,
            dto.Name,
            dto.Login,
            dto.Password,
            dto.Directory,
            dto.Site,
            dto.TotpSecret);
        
        await _loginSecretRepository.AddLoginDataAsync(loginSecret);
        return true;
    }

    public async Task<LoginSecretDto> GetSecretDto(Guid secretId)
    {
        var secret = await _loginSecretRepository.GetLoginDataByIdAsync(secretId);
        var loginSecretDto = new LoginSecretDto(secret);
        return loginSecretDto;
    }

    public async Task<List<LoginSecretDto>> GetSecretsDto(User user)
    {
        var secrets = await _loginSecretRepository.GetLoginDataListByUserIdAsync(user.Id);
        var secretsDto = new List<LoginSecretDto>();
        foreach (var secret in secrets)
        {
            secretsDto.Add(await GetSecretDto(secret.Id));
        }

        return secretsDto;
    }

    public async Task<bool> UpdateSecretDto(LoginSecretDto dto)
    {
        var secret = await _loginSecretRepository.GetLoginDataByIdAsync(dto.Id);
        if (secret == null)
            return false;
        
        secret.ChangeDirectory(dto.Directory);
        secret.ChangePassword(dto.Password);
        secret.ChangeSiteName(dto.Site);
        secret.ChangeTotpSecret(dto.TotpSecret);
        secret.ChangeLogin(dto.Login);
        secret.ChangeName(dto.Name);
        
        return true;
    }

    public async Task<bool> DeleteSecretByUserId(Guid userId)
    {
        await _loginSecretRepository.DeleteLoginDataByUserIdAsync(userId);
        return true;
    }
}