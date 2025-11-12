using Quiz.Application.DTOs.Question;
using SharedLibrary.Wrapper;

namespace Quiz.Application.Services.Question;

public interface IQuestionService
{
    Task<ApiResponse<Guid>> AddQuestionToQuizAsync(QuestionDto dto, Guid quizId);
    Task<ApiResponse> UpdateQuestionAsync(QuestionDto dto);
    Task<ApiResponse> DeleteQuestionAsync(Guid questionId);
    Task<ApiResponse<QuestionDto>> GetQuestionByIdAsync(Guid questionId);
}