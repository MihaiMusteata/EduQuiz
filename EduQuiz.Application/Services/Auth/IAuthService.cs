using EduQuiz.Application.DTOs.Auth;
using EduQuiz.Application.Wrapper;

namespace EduQuiz.Application.Services.Auth;

public interface IAuthService
{
    Task<ApiResponse<AuthResultDto>> RegisterAsync(RegisterDto dto);
    Task<ApiResponse<AuthResultDto>> LoginAsync(LoginDto dto);
}