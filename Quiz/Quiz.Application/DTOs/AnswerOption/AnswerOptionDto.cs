namespace Quiz.Application.DTOs.AnswerOption;

public class AnswerOptionDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public bool IsCorrect { get; set; }
}