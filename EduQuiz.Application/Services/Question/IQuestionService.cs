using EduQuiz.Application.DTOs.Question;
using EduQuiz.Application.Wrapper;

namespace EduQuiz.Application.Services.Question;

public interface IQuestionService
{
    Task<ApiResponse<Guid>> AddQuestionToQuizAsync(QuestionDto dto, Guid quizId);
    Task<ApiResponse> UpdateQuestionAsync(QuestionDto dto);
    Task<ApiResponse> DeleteQuestionAsync(Guid questionId);
    Task<ApiResponse<QuestionDto>> GetQuestionByIdAsync(Guid questionId);
}