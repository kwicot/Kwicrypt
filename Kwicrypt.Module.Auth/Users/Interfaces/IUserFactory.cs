using Backend.Modules.Data.Models;

namespace Backend.Modules.Data.Interfaces;

public interface IUserFactory
{
    User GetUser(string username, string password);
}