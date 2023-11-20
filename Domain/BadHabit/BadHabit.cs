﻿namespace Domain.BadHabit;

public sealed class BadHabit
{
    public required BadHabitId Id { get; set; }
    public required string Description { get; set; }
    public required DateTimeOffset StartDate { get; set; }
    public List<BadHabitOccurrence> Occurrences { get; } = new();
}