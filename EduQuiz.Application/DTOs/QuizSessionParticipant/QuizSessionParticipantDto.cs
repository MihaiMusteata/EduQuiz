namespace EduQuiz.Application.DTOs.QuizSessionParticipant;

public class QuizSessionParticipantDto
{
    public string DisplayName { get; set; }
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    public DateTime? FinishedAt { get; set; }
    public int TotalScore { get; set; }
}