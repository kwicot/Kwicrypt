using Kwicrypt.Module.Auth.ModelBinders;
using Microsoft.AspNetCore.Mvc;

namespace Kwicrypt.Module.Auth.Attributes;

public class EncryptedModelBinderAttribute : ModelBinderAttribute
{
    public EncryptedModelBinderAttribute() : base(typeof(EncryptedUserRegisterRequestModelBinder)) { }
}