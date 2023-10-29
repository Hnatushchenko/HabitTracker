using Domain.Habit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public sealed class HabitConfiguration : IEntityTypeConfiguration<Habit>
{
    public void Configure(EntityTypeBuilder<Habit> builder)
    {
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id)
            .HasConversion(habitId => habitId.Value, value => new HabitId(value));
        builder.Property(h => h.FrequencyCount)
            .HasConversion(frequencyCount => frequencyCount.Value, value => FrequencyCount.From(value));
        builder.HasMany(h => h.ToDoItems);
    }
}