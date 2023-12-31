﻿using Domain.Habit;
using Domain.Habit.ValueObjects;
using Infrastructure.ValueConverters;
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
        builder.HasMany(e => e.HabitArchivedPeriods)
            .WithOne(e => e.Habit)
            .HasForeignKey(e => e.HabitId);
        builder.Property(h => h.Id)
            .HasConversion(habitId => habitId.Value, value => new HabitId(value));
        builder.Property(h => h.FrequencyCount)
            .HasConversion(frequencyCount => frequencyCount.Value, value => FrequencyCount.From(value));
        builder.HasMany(h => h.ToDoItems);
        builder.Property(t => t.DefaultEndTime).HasConversion<TimeOnlyValueConverter>().HasColumnType("time");
        builder.Property(t => t.DefaultStartTime).HasConversion<TimeOnlyValueConverter>().HasColumnType("time");
        builder.ToTable(t => t.HasCheckConstraint(nameof(Habit.DefaultStartTime),
            $"{nameof(Habit.DefaultStartTime)} <= {nameof(Habit.DefaultEndTime)}"));
    }
}