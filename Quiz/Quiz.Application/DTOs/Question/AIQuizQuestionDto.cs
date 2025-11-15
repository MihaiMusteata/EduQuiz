using Quiz.Application.DTOs.AnswerOption;
using Quiz.Domain.Enums;

namespace Quiz.Application.DTOs.Question;

public class AIQuizQuestionDto
{
    public string Text { get; set; }
    public QuestionType Type { get; set; }
    public string Hint { get; set; }
    public List<AIAnswerDto> AnswerOptions { get; set; }
}