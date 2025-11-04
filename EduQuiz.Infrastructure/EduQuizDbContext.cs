using EduQuiz.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EduQuiz.Infrastructure;

public class EduQuizDbContext(DbContextOptions<EduQuizDbContext> options) : IdentityDbContext<UserDbTable>(options)
{
    public DbSet<QuizDbTable> Quizzes { get; set; }
    public DbSet<QuestionDbTable> Questions { get; set; }
    public DbSet<AnswerOptionDbTable> AnswerOptions { get; set; }
    public DbSet<QuizSessionDbTable> QuizSessions { get; set; }
    public DbSet<QuizSessionParticipantDbTable> QuizSessionUsers { get; set; }
    public DbSet<ParticipantAnswerDbTable> UserAnswers { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(EduQuizDbContext).Assembly);
    }
}