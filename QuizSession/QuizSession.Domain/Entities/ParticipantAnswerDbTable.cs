using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuizSession.Domain.ValueObjects;

namespace QuizSession.Domain.Entities;

public class ParticipantAnswerDbTable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public AnswerData AnswerData { get; set; }

    public bool IsCorrect { get; set; }
    public int EarnedPoints { get; set; }
    
    public Guid QuestionId { get; set; }

    [ForeignKey("Participant")]
    public Guid ParticipantId { get; set; }
    public QuizSessionParticipantDbTable Participant { get; set; }
}