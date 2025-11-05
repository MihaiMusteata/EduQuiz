using EduQuiz.Application.DTOs.AnswerOption;
using EduQuiz.Domain.Enums;

namespace EduQuiz.Application.DTOs.Question;

public class QuestionDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public QuestionType Type { get; set; }
    public int Points { get; set; }
    public IEnumerable<AnswerOptionDto> AnswerOptions { get; set; } = new List<AnswerOptionDto>();
}