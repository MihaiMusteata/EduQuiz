using Microsoft.EntityFrameworkCore;
using QuizSession.Domain.Entities;

namespace QuizSession.Infrastructure;

public class QuizSessionDbContext : DbContext
{
    public QuizSessionDbContext(DbContextOptions<QuizSessionDbContext> options)
        : base(options)
    {
    }

    public DbSet<QuizSessionParticipantDbTable> QuizSessionParticipants { get; set; }
    public DbSet<QuizSessionDbTable> QuizSessions { get; set; }
    public DbSet<ParticipantAnswerDbTable> ParticipantAnswers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ParticipantAnswerDbTable>(builder =>
        {
            builder.OwnsOne(u => u.AnswerData, nav => { nav.ToJson(); });
        });

        base.OnModelCreating(modelBuilder);
    }
}