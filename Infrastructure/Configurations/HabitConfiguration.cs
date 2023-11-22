using Domain.Habit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public sealed class HabitConfiguration : IEntityTypeConfiguration<Habit>
{
    public void Configure(EntityTypeBuilder<Habit> builder)
    {
        builder.HasKey(h => h.Id);
        builder.HasMany(e => e.ToDoItems)
            .WithOne(e => e.Habit)
            .HasForeignKey(e => e.HabitId)
            .IsRequired(false);
        builder.Property(h => h.Id)
            .HasConversion(habitId => habitId.Value, value => new HabitId(value));
        builder.Property(h => h.FrequencyCount)
            .HasConversion(frequencyCount => frequencyCount.Value, value => FrequencyCount.From(value));
        builder.HasMany(h => h.ToDoItems);
        builder.Property(t => t.DefaultEndTime).HasConversion<MyTimeOnlyConverter>().HasColumnType("time");
        builder.Property(t => t.DefaultStartTime).HasConversion<MyTimeOnlyConverter>().HasColumnType("time");
    }
}