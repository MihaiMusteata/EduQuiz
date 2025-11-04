using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduQuiz.Domain.Entities;

public class QuizSessionParticipantDbTable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string? DisplayName { get; set; }
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    public DateTime? FinishedAt { get; set; }

    public int TotalScore { get; set; }
    
    [ForeignKey("QuizSession")]
    public Guid QuizSessionId { get; set; }
    public QuizSessionDbTable QuizSession { get; set; }

    [ForeignKey("User")]
    public string? UserId { get; set; }
    public UserDbTable User { get; set; }
    
    [InverseProperty("Participant")]
    public ICollection<ParticipantAnswerDbTable> Answers { get; set; }
}