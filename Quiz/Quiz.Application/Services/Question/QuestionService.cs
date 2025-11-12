using Microsoft.EntityFrameworkCore;
using Quiz.Application.DTOs.Question;
using Quiz.Application.Mappers;
using Quiz.Infrastructure;
using SharedLibrary.Wrapper;

namespace Quiz.Application.Services.Question;

public class QuestionService(QuizDbContext _context) : IQuestionService
{
    public async Task<ApiResponse<Guid>> AddQuestionToQuizAsync(QuestionDto dto, Guid quizId)
    {
        var quiz = await _context.Quizzes.FindAsync(quizId);
        if (quiz is null)
        {
            return ApiResponse<Guid>.Fail("Quiz not found");
        }

        var question = dto.ToDbTable();
        question.QuizId = quizId;

        try
        {
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return ApiResponse<Guid>.Fail("Failed to create question due to database error");
        }

        return ApiResponse<Guid>.Ok(question.Id);
    }

    public async Task<ApiResponse> UpdateQuestionAsync(QuestionDto dto)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var oldQuestion = await _context.Questions
                .Include(q => q.AnswerOptions)
                .FirstOrDefaultAsync(q => q.Id == dto.Id);

            if (oldQuestion == null)
                return ApiResponse.Fail("Question not found");

            oldQuestion.Text = dto.Text;
            oldQuestion.Type = dto.Type;
            oldQuestion.Points = dto.Points;

            if (dto.AnswerOptions != null)
            {
                var answersToRemove = oldQuestion.AnswerOptions
                    .Where(a => dto.AnswerOptions.All(d => d.Id != a.Id))
                    .ToList();

                if (answersToRemove.Count != 0)
                {
                    _context.AnswerOptions.RemoveRange(answersToRemove);
                }

                foreach (var dtoOption in dto.AnswerOptions)
                {
                    var existingOption = oldQuestion.AnswerOptions
                        .FirstOrDefault(a => a.Id == dtoOption.Id);

                    if (existingOption != null)
                    {
                        existingOption.Text = dtoOption.Text;
                        existingOption.IsCorrect = dtoOption.IsCorrect;
                    }
                    else
                    {
                        var newOption = dtoOption.ToDbTable();
                        newOption.QuestionId = oldQuestion.Id;
                        _context.AnswerOptions.Add(newOption);
                    }
                }
            }
            else
            {
                if (oldQuestion.AnswerOptions.Count != 0)
                {
                    _context.AnswerOptions.RemoveRange(oldQuestion.AnswerOptions);
                }
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return ApiResponse.Ok("Question updated successfully");
        }
        catch (DbUpdateException ex)
        {
            await transaction.RollbackAsync();
            return ApiResponse.Fail("Failed to update question due to database error");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return ApiResponse.Fail("An unexpected error occurred");
        }
    }

    public async Task<ApiResponse> DeleteQuestionAsync(Guid questionId)
    {
        var question = await _context.Questions.FindAsync(questionId);
        if (question == null)
            return ApiResponse.Fail("Question not found");

        try
        {
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return ApiResponse.Fail("Failed to delete question due to database error");
        }

        return ApiResponse.Ok("Question deleted successfully");
    }

    public async Task<ApiResponse<QuestionDto>> GetQuestionByIdAsync(Guid questionId)
    {
        var question = await _context.Questions
            .Include(q => q.AnswerOptions)
            .FirstOrDefaultAsync(q => q.Id == questionId);

        if (question is null)
            return ApiResponse<QuestionDto>.Fail("Question not found");

        var questionDto = question.ToDto();

        return ApiResponse<QuestionDto>.Ok(questionDto);
    }
}