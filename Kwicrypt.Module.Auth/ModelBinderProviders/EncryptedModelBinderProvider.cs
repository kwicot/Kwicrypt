using Kwicrypt.Module.Auth.Dtos;
using Kwicrypt.Module.Auth.ModelBinders;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Kwicrypt.Module.Auth.ModelBinderProviders;

public class EncryptedModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(UserRegisterRequestDto))
        {
            return new BinderTypeModelBinder(typeof(EncryptedUserRegisterRequestModelBinder));
        }

        return null;
    }
}