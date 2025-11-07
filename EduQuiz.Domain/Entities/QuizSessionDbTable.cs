using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EduQuiz.Domain.Enums;

namespace EduQuiz.Domain.Entities;

public class QuizSessionDbTable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string AccessCode { get; set; }

    public QuizSessionStatus Status { get; set; }

    [ForeignKey("Quiz")]
    public Guid QuizId { get; set; }
    public QuizDbTable Quiz { get; set; }
    
    [ForeignKey("HostUser")]
    public string HostUserId { get; set; }
    public UserDbTable HostUser { get; set; }

    [InverseProperty("QuizSession")]
    public ICollection<QuizSessionParticipantDbTable> Participants { get; set; }
}