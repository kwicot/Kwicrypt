using System.Threading.Tasks;

namespace Core
{
    public interface IAuthService
    {
        public Task<bool> RegisterUsingMailAsync(string mail, string password);
        public Task<bool> LoginUsingMailAsync(string mail, string password);
        public Task<bool> LogoutAsync();
        public Task<bool> RevokeRefreshTokensAsync();
    }
}