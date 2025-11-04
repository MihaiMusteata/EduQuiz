using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduQuiz.Domain.Entities;

public class AnswerOptionDbTable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string Text { get; set; }
    public bool IsCorrect { get; set; }
    
    [ForeignKey("Question")]
    public Guid QuestionId { get; set; }
    public QuestionDbTable Question { get; set; }
}