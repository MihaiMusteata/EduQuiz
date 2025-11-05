using EduQuiz.Application.DTOs.Question;

namespace EduQuiz.Application.DTOs.Quiz;

public class QuizDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsPublic { get; set; }
    public DateTime CreatedAt { get; set; }
    public IEnumerable<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
}

