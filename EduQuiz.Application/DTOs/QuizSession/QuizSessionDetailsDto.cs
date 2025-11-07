using EduQuiz.Application.DTOs.QuizSessionParticipant;
using EduQuiz.Domain.Enums;

namespace EduQuiz.Application.DTOs.QuizSession;

public class QuizSessionDetailsDto : QuizSessionDto
{
    public string AccessCode { get; set; }
    public QuizSessionStatus Status { get; set; }
    public IEnumerable<QuizSessionParticipantDto> Participants { get; set; } = new List<QuizSessionParticipantDto>();
}