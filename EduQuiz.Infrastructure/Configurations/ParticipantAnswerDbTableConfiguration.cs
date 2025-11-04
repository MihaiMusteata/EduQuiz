using EduQuiz.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduQuiz.Infrastructure.Configurations;

public class ParticipantAnswerDbTableConfiguration: IEntityTypeConfiguration<ParticipantAnswerDbTable>
{
    public void Configure(EntityTypeBuilder<ParticipantAnswerDbTable> builder)
    {
        builder.ToTable("ParticipantAnswers");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.IsCorrect)
            .IsRequired();

        builder.Property(u => u.EarnedPoints)
            .IsRequired();

        builder.HasOne(u => u.Question)
            .WithMany(q => q.UserAnswers)
            .HasForeignKey(u => u.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(u => u.Participant)
            .WithMany(p => p.Answers)
            .HasForeignKey(u => u.ParticipantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(u => u.AnswerData, nav =>
        {
            nav.ToJson();
        });

        builder.HasIndex(u => u.QuestionId);
        builder.HasIndex(u => u.ParticipantId);
    }
}
