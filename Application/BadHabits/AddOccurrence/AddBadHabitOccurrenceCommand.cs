﻿using Domain.BadHabit;
using MediatR;

namespace Application.BadHabits.AddOccurrence;

public sealed record AddBadHabitOccurrenceCommand : IRequest
{
    public required DateTimeOffset OccurrenceDate { get; init; }
    public required BadHabitId BadHabitId { get; init; }
}