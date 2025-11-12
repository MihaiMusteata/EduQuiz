using QuizSession.Application.DTOs.QuizSession;
using SharedLibrary.Wrapper;

namespace QuizSession.Application.Services.QuizSession;

public interface IQuizSessionService
{
    Task<ApiResponse<string>> CreateNewSession(QuizSessionDto quizSessionDto, string hostUserId);
    Task<ApiResponse<QuizSessionDto>> GetSessionByIdAsync(Guid sessionId, string? hostUserId = null);
    Task<ApiResponse> UpdateSessionStatusAsync(Guid sessionId, string? hostUserId = null);
}