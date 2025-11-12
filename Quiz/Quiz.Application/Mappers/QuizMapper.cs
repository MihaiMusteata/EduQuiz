using Quiz.Application.DTOs.Quiz;
using Quiz.Domain.Entities;

namespace Quiz.Application.Mappers;

public static class QuizMapper
{
    public static QuizDto ToDto(this QuizDbTable quiz)
    {
        return new QuizDto
        {
            Id = quiz.Id,
            Title = quiz.Title,
            Description = quiz.Description,
            IsPublic = quiz.IsPublic,
            CreatedAt = quiz.CreatedAt,
            Questions = quiz.Questions.Select(q => q.ToDto()).ToList()
        };
    }

    public static QuizDbTable ToDbTable(this QuizDto dto)
    {
        return new QuizDbTable
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
            IsPublic = dto.IsPublic,
            Questions = dto.Questions.Select(q => q.ToDbTable()).ToList()
        };
    }
}