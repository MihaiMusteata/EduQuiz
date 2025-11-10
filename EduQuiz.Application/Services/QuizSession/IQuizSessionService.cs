using EduQuiz.Application.DTOs.QuizSession;
using EduQuiz.Application.Wrapper;

namespace EduQuiz.Application.Services.QuizSession;

public interface IQuizSessionService
{
    Task<ApiResponse<string>> CreateNewSession(QuizSessionDto quizSessionDto, string hostUserId);
    Task<ApiResponse<QuizSessionDto>> GetSessionByIdAsync(Guid sessionId, string? hostUserId = null);
    Task<ApiResponse> UpdateSessionStatusAsync(Guid sessionId, string? hostUserId = null);
}