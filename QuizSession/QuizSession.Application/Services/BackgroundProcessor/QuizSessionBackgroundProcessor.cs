using Microsoft.EntityFrameworkCore;
using QuizSession.Domain.Enums;
using QuizSession.Infrastructure;
using SharedLibrary.Wrapper;

namespace QuizSession.Application.Services.BackgroundProcessor;

public class QuizSessionBackgroundProcessor(QuizSessionDbContext _context) : IQuizSessionBackgroundProcessor
{
    public async Task<ApiResponse> StartQuizSessionAsync(Guid sessionId)
    {
        var session = await _context.QuizSessions
            .Include(s => s.Participants)
            .FirstOrDefaultAsync(s => s.Id == sessionId);

        if (session is null)
            return ApiResponse.Fail("Quiz session not found");

        if (session.Status != QuizSessionStatus.WaitingForParticipants)
            return ApiResponse.Fail("Quiz session cannot be started");

        session.Status = QuizSessionStatus.InProgress;
        session.StartTime = DateTime.UtcNow;

        try
        {
            _context.QuizSessions.Update(session);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return ApiResponse.Fail("Failed to start quiz session due to database error");
        }


        return ApiResponse.Ok("Quiz session started successfully");
    }

    public async Task<ApiResponse> EndQuizSessionAsync(Guid sessionId)
    {
        var session = await _context.QuizSessions
            .Include(s => s.Participants)
            .FirstOrDefaultAsync(s => s.Id == sessionId);

        if (session is null)
            return ApiResponse.Fail("Quiz session not found");

        if (session.Status != QuizSessionStatus.InProgress)
            return ApiResponse.Fail("Quiz session is not in progress");

        session.Status = QuizSessionStatus.Completed;
        session.EndTime = DateTime.UtcNow;

        try
        {
            _context.QuizSessions.Update(session);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return ApiResponse.Fail("Failed to end quiz session due to database error");
        }


        return ApiResponse.Ok("Quiz session ended successfully");
    }
}