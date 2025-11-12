using Microsoft.EntityFrameworkCore;
using Quiz.Domain.Entities;

namespace Quiz.Infrastructure;


public class QuizDbContext : DbContext
{
    public QuizDbContext(DbContextOptions<QuizDbContext> options)
        : base(options)
    {
    }

    public DbSet<QuizDbTable> Quizzes { get; set; }
    public DbSet<QuestionDbTable> Questions { get; set; }
    public DbSet<AnswerOptionDbTable> AnswerOptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}