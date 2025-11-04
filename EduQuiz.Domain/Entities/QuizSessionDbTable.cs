using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduQuiz.Domain.Entities;

public class QuizSessionDbTable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }

    public bool IsActive => EndTime == null || DateTime.UtcNow < EndTime.Value;

    [ForeignKey("Quiz")]
    public Guid QuizId { get; set; }
    public QuizDbTable Quiz { get; set; }

    [InverseProperty("QuizSession")]
    public ICollection<QuizSessionParticipantDbTable> Participants { get; set; }
}