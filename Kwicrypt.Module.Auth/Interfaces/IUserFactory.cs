using Kwicrypt.Module.Auth.Models;

namespace Kwicrypt.Module.Auth.Interfaces;

public interface IUserFactory
{
    User GetUser(string username, string password);
}