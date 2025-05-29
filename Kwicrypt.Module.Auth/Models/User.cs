
using System.Security.Cryptography;
using Kwicrypt.Module.Core;

namespace Kwicrypt.Module.Auth.Models;

public class User : DbModelBase
{
    
    public string Mail { get; private set; }
    public string Username { get; private set; }
    public string PasswordHash { get; private set; }
    public string PublicRsaKey { get; private set; }
    
    public bool MailVerified { get; private set; }
    
    
    public bool UseEncryption => !string.IsNullOrEmpty(PublicRsaKey.ToString());
    
    public User(){}
    
    public User(Guid id, string mail, string passwordHash, string publicRsaKey) : base(id)
    {
        Mail = mail;
        PasswordHash = passwordHash;
        Username = string.Empty;

        PublicRsaKey = publicRsaKey;
        
        MailVerified = false;
    }

    public void SetPublicRSAKey(string publicKey)
    {
        PublicRsaKey = publicKey;
    }

    public void ChangeMail(string newMail)
    {
        Mail = newMail;
        MailVerified = false;
    }

    public void VerifyMail()
    {
        MailVerified = true;
    }
    
}