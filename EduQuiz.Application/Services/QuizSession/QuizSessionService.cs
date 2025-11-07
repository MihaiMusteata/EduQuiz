using EduQuiz.Application.DTOs.QuizSession;
using EduQuiz.Application.Mappers;
using EduQuiz.Application.Wrapper;
using EduQuiz.Domain.Entities;
using EduQuiz.Domain.Enums;
using EduQuiz.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EduQuiz.Application.Services.QuizSession;

public class QuizSessionService(EduQuizDbContext _context) : IQuizSessionService
{
    public async Task<ApiResponse<string>> CreateNewSession(QuizSessionDto quizSessionDto, string hostUserId)
    {
        var newSession = new QuizSessionDbTable
        {
            Id = Guid.NewGuid(),
            QuizId = quizSessionDto.QuizId,
            HostUserId = hostUserId,
            StartTime = quizSessionDto.StartTime,
            EndTime = quizSessionDto.EndTime,
            AccessCode = GeneratePin(),
            Status = QuizSessionStatus.WaitingForParticipants
        };

        try
        {
            await _context.QuizSessions.AddAsync(newSession);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return ApiResponse<string>.Fail("Failed to create quiz session due to database error");
        }

        return ApiResponse<string>.Ok(newSession.AccessCode);
    }

    public async Task<ApiResponse<QuizSessionDto>> GetSessionByIdAsync(Guid sessionId, string? hostUserId = null)
    {
        var session = await _context.QuizSessions
            .Include(s => s.Participants)
            .FirstOrDefaultAsync(s => s.Id == sessionId);

        if (session is null)
            return ApiResponse<QuizSessionDto>.Fail("Quiz session not found");

        if (hostUserId is not null && session.HostUserId == hostUserId)
        {
            var sessionDetailsDto = session.ToDetailsDto();
            return ApiResponse<QuizSessionDto>.Ok(sessionDetailsDto);
        }

        var sessionDto = session.ToDto();

        return ApiResponse<QuizSessionDto>.Ok(sessionDto);
    }

    public async Task<ApiResponse> UpdateSessionStatusAsync(Guid sessionId, string? hostUserId = null)
    {
        var session = await _context.QuizSessions.FindAsync(sessionId);
        if (session is null)
            return ApiResponse.Fail("Quiz session not found");

        if (hostUserId is not null && session.HostUserId != hostUserId)
            return ApiResponse.Fail("Unauthorized to update this session");

        if (session.Status == QuizSessionStatus.Completed)
            return ApiResponse.Fail("Quiz session is already completed");

        session.Status++;

        if (session.Status == QuizSessionStatus.Completed)
            session.EndTime = DateTime.UtcNow;

        try
        {
            _context.QuizSessions.Update(session);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return ApiResponse.Fail("Failed to update quiz session due to database error");
        }
        
        return ApiResponse.Ok("Quiz session updated successfully");
    }

    private string GeneratePin()
    {
        var random = new Random();
        return random.Next(100000, 999999).ToString();
    }
}