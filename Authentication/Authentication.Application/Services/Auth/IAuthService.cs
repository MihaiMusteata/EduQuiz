using Authentication.Application.DTOs.Auth;
using SharedLibrary.Wrapper;

namespace Authentication.Application.Services.Auth;

public interface IAuthService
{
    Task<ApiResponse<AuthResultDto>> RegisterAsync(RegisterDto dto);
    Task<ApiResponse<AuthResultDto>> LoginAsync(LoginDto dto);
}