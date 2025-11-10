using EduQuiz.Application.Wrapper;

namespace EduQuiz.Application.Services.BackgroundProcessor;

public interface IQuizSessionBackgroundProcessor
{
    Task<ApiResponse> StartQuizSessionAsync(Guid sessionId);
    Task<ApiResponse> EndQuizSessionAsync(Guid sessionId);
}