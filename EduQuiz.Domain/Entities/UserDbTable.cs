using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EduQuiz.Domain.Entities;

public class UserDbTable : IdentityUser
{
    public ICollection<QuizDbTable> CreatedQuizzes { get; set; }
    public ICollection<QuizSessionParticipantDbTable> Participations { get; set; }
}