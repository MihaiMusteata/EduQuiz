using EduQuiz.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduQuiz.Infrastructure.Configurations;

public class QuizDbTableConfiguration : IEntityTypeConfiguration<QuizDbTable>
{
    public void Configure(EntityTypeBuilder<QuizDbTable> builder)
    {
        builder.ToTable("Quizzes");

        builder.HasKey(q => q.Id);

        builder.Property(q => q.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(q => q.Description)
            .HasMaxLength(1000);

        builder.Property(q => q.IsPublic)
            .IsRequired();

        builder.Property(q => q.CreatedAt)
            .IsRequired();

        builder.HasOne(q => q.CreatedByUser)
            .WithMany()
            .HasForeignKey(q => q.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(q => q.Questions)
            .WithOne(qst => qst.Quiz)
            .HasForeignKey(qst => qst.QuizId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(q => q.QuizSessions)
            .WithOne(s => s.Quiz)
            .HasForeignKey(s => s.QuizId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(q => q.Title);
        builder.HasIndex(q => q.IsPublic);
        builder.HasIndex(q => q.CreatedByUserId);
    }
}