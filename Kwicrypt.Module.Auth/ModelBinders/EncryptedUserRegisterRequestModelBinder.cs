using System.Text;
using System.Text.Json;
using Kwicrypt.Module.Auth.Dtos;
using Microsoft.AspNetCore.Mvc.ModelBinding;

#if MODULE_CRYPTO
using Kwicrypt.Module.Cryptography.Interfaces;
#endif

namespace Kwicrypt.Module.Auth.ModelBinders;

public class EncryptedUserRegisterRequestModelBinder : IModelBinder
{
    private readonly ICryptoService _cryptoService;
    
    public EncryptedUserRegisterRequestModelBinder(ICryptoService cryptoService)
    {
        _cryptoService = cryptoService;
    }
    
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var request = bindingContext.HttpContext.Request;
        request.EnableBuffering();

        using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
        var encryptedJson = reader.ReadToEnd();
        request.Body.Position = 0;
        
        //AES128
        //AES256
        //RSA2048
        //RSA4096
        //Base64
        //NONE
        var encrypted = request.HttpContext.Request.Headers["X-Encrypted"].FirstOrDefault(); 
        var encryptionMethod = request.HttpContext.Request.Headers["X-Encryption-Method"].FirstOrDefault();
        

        var decryptedJson = Decrypt(encryptedJson); // твоя реализация

        var dto = JsonSerializer.Deserialize<UserRegisterRequestDto>(decryptedJson);

        bindingContext.Result = ModelBindingResult.Success(dto);
        return Task.CompletedTask;
    }
}