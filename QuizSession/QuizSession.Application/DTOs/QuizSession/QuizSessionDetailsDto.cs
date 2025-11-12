using QuizSession.Application.DTOs.QuizSessionParticipant;
using QuizSession.Domain.Enums;

namespace QuizSession.Application.DTOs.QuizSession;

public class QuizSessionDetailsDto : QuizSessionDto
{
    public string AccessCode { get; set; }
    public QuizSessionStatus Status { get; set; }
    public IEnumerable<QuizSessionParticipantDto> Participants { get; set; } = new List<QuizSessionParticipantDto>();
}