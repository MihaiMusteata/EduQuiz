using EduQuiz.Application.DTOs.Quiz;
using EduQuiz.Application.Wrapper;

namespace EduQuiz.Application.Services.Quiz;

public interface IQuizService
{
    Task<ApiResponse<Guid>> CreateQuizAsync(QuizDto quizDto, string userId);
    Task<ApiResponse<IEnumerable<QuizDto>>> GetAllQuizzesAsync(string? userId = null, bool onlyPublic = false);
    Task<ApiResponse<QuizDto>> GetQuizByIdAsync(Guid quizId);
    Task<ApiResponse> UpdateQuizAsync(QuizDto quizDto);
    Task<ApiResponse> DeleteQuizAsync(Guid quizId);
}