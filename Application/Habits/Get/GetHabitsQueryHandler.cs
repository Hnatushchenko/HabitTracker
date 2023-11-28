using Application.Habits.Statistic.Get;
using Domain.Habit;
using MediatR;

namespace Application.Habits.Get;

public sealed class GetHabitsQueryHandler : IRequestHandler<GetHabitsQuery, IEnumerable<HabitResponse>>
{
    private readonly IGoodHabitStreakCalculator _goodHabitStreakCalculator;
    private readonly IHabitRepository _habitRepository;

    public GetHabitsQueryHandler(IGoodHabitStreakCalculator goodHabitStreakCalculator,
        IHabitRepository habitRepository)
    {
        _goodHabitStreakCalculator = goodHabitStreakCalculator;
        _habitRepository = habitRepository;
    }
    
    public async Task<IEnumerable<HabitResponse>> Handle(GetHabitsQuery request, CancellationToken cancellationToken)
    {
        var habits = await _habitRepository.GetAllHabitsWithToDoItemsIncludedAsync(cancellationToken);
        var habitResponseList = new List<HabitResponse>(habits.Count);
        foreach (var habit in habits)
        {
            var streak = _goodHabitStreakCalculator.GetHabitStreak(habit);
            var habitResponse = new HabitResponse
            {
                Id = habit.Id.Value,
                Description = habit.Description,
                FrequencyTimeUnit = habit.FrequencyTimeUnit,
                FrequencyCount = habit.FrequencyCount.Value,
                Streak = streak,
                DefaultStartTime = habit.DefaultStartTime,
                DefaultEndTime = habit.DefaultEndTime,
                ToDoItemDescription = habit.ToDoItemDescription
            };
            habitResponseList.Add(habitResponse);
        }
        return habitResponseList;
    }
}