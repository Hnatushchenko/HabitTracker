using Application.Habits.Statistic.Get;
using Domain.Habit;
using MediatR;

namespace Application.Habits.Get;

public sealed class GetHabitsQueryHandler : IRequestHandler<GetHabitsQuery, IEnumerable<HabitResponse>>
{
    private readonly IHabitRepository _habitRepository;
    private readonly ISender _sender;

    public GetHabitsQueryHandler(IHabitRepository habitRepository,
        ISender sender)
    {
        _habitRepository = habitRepository;
        _sender = sender;
    }
    public async Task<IEnumerable<HabitResponse>> Handle(GetHabitsQuery request, CancellationToken cancellationToken)
    {
        var habits = await _habitRepository.GetAllAsync();
        var habitResponseList = new List<HabitResponse>(habits.Count);
        foreach (var habit in habits)
        {
            var statistic = await _sender.Send(new GetHabitStatisticQuery(habit.Id), cancellationToken);
            var datesOnly = statistic.DateBasedHabitStatuses
                .Where(dateBasedHabitStatus => dateBasedHabitStatus.IsCompleted)
                .Select(dateBasedHabitStatus => dateBasedHabitStatus.Date.Date).ToHashSet();
            var yesterday = DateTimeOffset.UtcNow.AddDays(-1).UtcDateTime.Date;
            var streak = 0;
            if (datesOnly.Contains(DateTimeOffset.UtcNow.UtcDateTime.Date))
            {
                streak++;
            }
            if (datesOnly.Contains(yesterday))
            {
                streak++;
                while (datesOnly.Contains(yesterday.AddDays(-1)))
                {
                    streak++;
                    yesterday = yesterday.AddDays(-1);
                }
            }
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