using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Quiz.Domain.Enums;

namespace Quiz.Domain.Entities;

public class QuestionDbTable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string Text { get; set; }
    public QuestionType Type { get; set; }
    public int Points { get; set; }

    [ForeignKey("Quiz")]
    public Guid QuizId { get; set; }
    public QuizDbTable Quiz { get; set; }

    [InverseProperty("Question")]
    public ICollection<AnswerOptionDbTable> AnswerOptions { get; set; } 
}