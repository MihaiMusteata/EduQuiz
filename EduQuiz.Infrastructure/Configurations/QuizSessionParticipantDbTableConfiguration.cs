using EduQuiz.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduQuiz.Infrastructure.Configurations;

public class QuizSessionParticipantDbTableConfiguration : IEntityTypeConfiguration<QuizSessionParticipantDbTable>
{
    public void Configure(EntityTypeBuilder<QuizSessionParticipantDbTable> builder)
    {
        builder.ToTable("QuizSessionParticipants");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.JoinedAt)
            .IsRequired();

        builder.HasOne(p => p.QuizSession)
            .WithMany(s => s.Participants)
            .HasForeignKey(p => p.QuizSessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Answers)
            .WithOne(a => a.Participant)
            .HasForeignKey(a => a.ParticipantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(p => p.QuizSessionId);
        builder.HasIndex(p => p.UserId);
    }
}