using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EduQuiz.Domain.ValueObjects;

namespace EduQuiz.Domain.Entities;

public class ParticipantAnswerDbTable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public AnswerData AnswerData { get; set; }

    public bool IsCorrect { get; set; }
    public int EarnedPoints { get; set; }
    
    [ForeignKey("Question")]
    public Guid QuestionId { get; set; }
    public QuestionDbTable Question { get; set; }

    [ForeignKey("Participant")]
    public Guid ParticipantId { get; set; }
    public QuizSessionParticipantDbTable Participant { get; set; }
    
    public ICollection<AnswerOptionDbTable> SelectedOptions { get; set; }
}