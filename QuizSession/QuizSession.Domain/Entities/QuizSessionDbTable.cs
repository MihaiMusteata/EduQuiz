using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuizSession.Domain.Enums;

namespace QuizSession.Domain.Entities;

public class QuizSessionDbTable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string AccessCode { get; set; }

    public QuizSessionStatus Status { get; set; }

    public Guid QuizId { get; set; }
    
    public string HostUserId { get; set; }

    public ICollection<QuizSessionParticipantDbTable> Participants { get; set; }
}