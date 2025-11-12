using QuizSession.Application.DTOs.QuizSession;
using QuizSession.Domain.Entities;

namespace QuizSession.Application.Mappers;

public static class QuizSessionMapper
{
    public static QuizSessionDto ToDto(this QuizSessionDbTable quizSession)
    {
        return new QuizSessionDto
        {
            Id = quizSession.Id,
            QuizId = quizSession.QuizId,
            StartTime = quizSession.StartTime,
            EndTime = quizSession.EndTime
        };
    }

    public static QuizSessionDetailsDto ToDetailsDto(this QuizSessionDbTable quizSession)
    {
        return new QuizSessionDetailsDto
        {
            Id = quizSession.Id,
            QuizId = quizSession.QuizId,
            StartTime = quizSession.StartTime,
            EndTime = quizSession.EndTime,
            AccessCode = quizSession.AccessCode,
            Status = quizSession.Status
        };
    }
    
}