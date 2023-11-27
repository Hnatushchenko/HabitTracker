using Domain.BadHabit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public sealed class BadHabitOccurrenceConfiguration : IEntityTypeConfiguration<BadHabitOccurrence>
{
    public void Configure(EntityTypeBuilder<BadHabitOccurrence> builder)
    {
        builder.HasKey(badHabitOccurrence => new { badHabitOccurrence.BadHabitId, badHabitOccurrence.OccurrenceDate });
    }
}