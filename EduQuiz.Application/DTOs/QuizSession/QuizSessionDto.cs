namespace EduQuiz.Application.DTOs.QuizSession;

public class QuizSessionDto
{
    public Guid Id { get; set; }
    public Guid QuizId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}