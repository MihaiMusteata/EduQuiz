using EduQuiz.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduQuiz.Infrastructure.Configurations;

public class QuizSessionDbTableConfiguration: IEntityTypeConfiguration<QuizSessionDbTable>
{
    public void Configure(EntityTypeBuilder<QuizSessionDbTable> builder)
    {
        builder.ToTable("QuizSessions");

        builder.HasKey(qs => qs.Id);

        builder.Property(qs => qs.StartTime)
            .IsRequired();

        builder.Property(qs => qs.EndTime)
            .IsRequired(false);

        builder.Property(qs => qs.AccessCode)
            .IsRequired()
            .HasMaxLength(16); 

        builder.HasOne(qs => qs.Quiz)
            .WithMany(q => q.QuizSessions)
            .HasForeignKey(qs => qs.QuizId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(qs => qs.HostUser)
            .WithMany(u => u.HostedSessions)
            .HasForeignKey(qs => qs.HostUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(qs => qs.Participants)
            .WithOne(p => p.QuizSession)
            .HasForeignKey(p => p.QuizSessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(qs => qs.AccessCode)
            .IsUnique();

    }
}