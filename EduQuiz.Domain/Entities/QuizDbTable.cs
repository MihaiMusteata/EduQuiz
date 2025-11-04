using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduQuiz.Domain.Entities;

public class QuizDbTable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }

    public bool IsPublic { get; set; } = true;
    public int TotalPoints { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("CreatedBy")]
    public string CreatedByUserId { get; set; }
    public UserDbTable CreatedByUser { get; set; }

    public ICollection<QuestionDbTable> Questions { get; set; }
    public ICollection<QuizSessionDbTable> QuizSessions { get; set; }

}
