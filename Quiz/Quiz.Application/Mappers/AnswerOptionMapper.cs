using Quiz.Application.DTOs.AnswerOption;
using Quiz.Domain.Entities;

namespace Quiz.Application.Mappers;

public static class AnswerOptionMapper
{
    public static AnswerOptionDto ToDto(this AnswerOptionDbTable answerOption)
    {
        return new AnswerOptionDto
        {
            Id = answerOption.Id,
            Text = answerOption.Text,
            IsCorrect = answerOption.IsCorrect
        };
    }

    public static AnswerOptionDbTable ToDbTable(this AnswerOptionDto dto)
    {
        return new AnswerOptionDbTable
        {
            Id = dto.Id,
            Text = dto.Text,
            IsCorrect = dto.IsCorrect
        };
    }
}