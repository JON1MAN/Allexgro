using Microsoft.AspNetCore.Identity;
public interface IAuthService
{
    public Task<Response<TokenResponseDTO>> RegisterUser(UserRegistrationDTO dto);
    public Task<Response<TokenResponseDTO>> LoginUser(UserLoginDTO dto);
}