using Microsoft.AspNetCore.Mvc;

namespace Kwicrypt.Module.Auth.Models;

public class AuthenticatedUser
{
    public User? User { get;}
    public IActionResult? ErrorResult { get;}

    public AuthenticatedUser(User? user, IActionResult? errorResult)
    {
        User = user;
        ErrorResult = errorResult;
    }
}