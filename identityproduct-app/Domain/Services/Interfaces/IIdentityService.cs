using identityproduct_app.Domain.Dto.Create;
using identityproduct_app.Domain.Dto.Read;

namespace identityproduct_app.Domain.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<UserRegisterResponse> UserRegister(UserRegisterRequest userRegister);
        Task<UserLoginResponse> UserLogin(UserLoginRequest userLogin);
    }
}
