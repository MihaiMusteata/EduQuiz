using Microsoft.EntityFrameworkCore;
using Quiz.Application.DTOs.Quiz;
using Quiz.Application.Mappers;
using Quiz.Infrastructure;
using SharedLibrary.Wrapper;

namespace Quiz.Application.Services.Quiz;

public class QuizService(QuizDbContext _context, HttpClient httpClient) : IQuizService
{
    public async Task<ApiResponse<Guid>> CreateQuizAsync(QuizDto quizDto, string userId)
    {
        var entity = quizDto.ToDbTable();
        entity.CreatedByUserId = userId;

        try
        {
            await _context.Quizzes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return ApiResponse<Guid>.Fail("Failed to create quiz due to database error");
        }

        return ApiResponse<Guid>.Ok(entity.Id);
    }

    public async Task<ApiResponse<IEnumerable<QuizDto>>> GetAllQuizzesAsync(string? userId = null, bool onlyPublic = false)
    {
        var query = _context.Quizzes
            .Include(q => q.Questions)
            .ThenInclude(qst => qst.AnswerOptions)
            .AsQueryable();

        if (userId is not null)
            query = query.Where(q => q.CreatedByUserId == userId);

        if (onlyPublic)
            query = query.Where(q => q.IsPublic);

        var quizzes = await query
            .Select(q => q.ToDto())
            .ToListAsync();
        
        return ApiResponse<IEnumerable<QuizDto>>.Ok(quizzes);
    }

    public async Task<ApiResponse<QuizDto>> GetQuizByIdAsync(Guid quizId)
    {
        var quiz = await _context.Quizzes
            .Include(q => q.Questions)
            .ThenInclude(qst => qst.AnswerOptions)
            .FirstOrDefaultAsync(q => q.Id == quizId);
        if (quiz is null)
            return ApiResponse<QuizDto>.Fail("Quiz not found");

        var quizDto = quiz.ToDto();

        return ApiResponse<QuizDto>.Ok(quizDto);
    }

    public async Task<ApiResponse> UpdateQuizAsync(QuizDto quiz)
    {
        var existingQuiz = await _context.Quizzes.FindAsync(quiz.Id);
        if (existingQuiz == null)
            return ApiResponse.Fail("Quiz not found");

        existingQuiz.Title = quiz.Title;
        existingQuiz.Description = quiz.Description;
        existingQuiz.IsPublic = quiz.IsPublic;

        try
        {
            _context.Quizzes.Update(existingQuiz);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return ApiResponse.Fail("Failed to update quiz due to database error");
        }

        return ApiResponse.Ok("Quiz updated successfully");
    }

    public async Task<ApiResponse> DeleteQuizAsync(Guid quizId)
    {
        var quiz = await _context.Quizzes.FindAsync(quizId);
        if (quiz == null)
            return ApiResponse.Fail("Quiz not found");

        try
        {
            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();
        } catch (DbUpdateException)
        {
            return ApiResponse.Fail("Failed to delete quiz due to database error");
        }

        return ApiResponse.Ok("Quiz deleted successfully");
    }
}