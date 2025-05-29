using System.Security.Cryptography;
using Kwicrypt.Module.Auth.Models;

namespace Kwicrypt.Module.Auth.Interfaces;

public interface IUserFactory
{
    User GetUser(string mail, string password, string publicRsaKey);
}