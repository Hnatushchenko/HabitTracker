using Domain.Habit;
using MediatR;

namespace Application.Habits.Get;

public sealed class GetHabitsQueryHandler : IRequestHandler<GetHabitsQuery, HabitsResponse>
{
    private readonly IGoodHabitStreakCalculator _goodHabitStreakCalculator;
    private readonly IHabitRepository _habitRepository;

    public GetHabitsQueryHandler(IGoodHabitStreakCalculator goodHabitStreakCalculator,
        IHabitRepository habitRepository)
    {
        _goodHabitStreakCalculator = goodHabitStreakCalculator;
        _habitRepository = habitRepository;
    }
    
    public async Task<HabitsResponse> Handle(GetHabitsQuery request, CancellationToken cancellationToken)
    {
        var habits = await _habitRepository.GetAllHabitsWithToDoItemsIncludedAsync(cancellationToken);
        var habitResponseList = habits.Select(habit =>
        {
            var streak = _goodHabitStreakCalculator.GetHabitStreak(habit);
            return new HabitResponse
            {
                ToDoItemDescription = habit.ToDoItemDescription,
                FrequencyTimeUnit = habit.FrequencyTimeUnit,
                FrequencyCount = habit.FrequencyCount.Value,
                DefaultStartTime = habit.DefaultStartTime,
                DefaultEndTime = habit.DefaultEndTime,
                Description = habit.Description,
                IsArchived = habit.IsArchived,
                Id = habit.Id.Value,
                Streak = streak
            };
        });

        var habitsResponse = new HabitsResponse(habitResponseList);
        return habitsResponse;
    }
}