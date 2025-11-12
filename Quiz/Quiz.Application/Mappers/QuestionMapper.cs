using Quiz.Application.DTOs.Question;
using Quiz.Domain.Entities;

namespace Quiz.Application.Mappers;

public static class QuestionMapper
{
    public static QuestionDto ToDto(this QuestionDbTable question)
    {
        return new QuestionDto
        {
            Id = question.Id,
            Text = question.Text,
            Type = question.Type,
            Points = question.Points,
            AnswerOptions = question.AnswerOptions.Select(a => a.ToDto()).ToList()
        };
    }
    
    public static QuestionDbTable ToDbTable(this QuestionDto dto)
    {
        return new QuestionDbTable
        {
            Text = dto.Text,
            Type = dto.Type,
            Points = dto.Points,
            AnswerOptions = dto.AnswerOptions.Select(a => a.ToDbTable()).ToList()
        };
    }
}