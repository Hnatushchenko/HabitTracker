using Domain.Habit;
using MediatR;

namespace Application.Habits.Get;

public sealed class GetHabitsQueryHandler : IRequestHandler<GetHabitsQuery, IEnumerable<HabitResponse>>
{
    private readonly IHabitRepository _habitRepository;

    public GetHabitsQueryHandler(IHabitRepository habitRepository)
    {
        _habitRepository = habitRepository;
    }
    public async Task<IEnumerable<HabitResponse>> Handle(GetHabitsQuery request, CancellationToken cancellationToken)
    {
        var habits = await _habitRepository.GetAllAsync();
        var habitResponseList = habits.Select(habit => new HabitResponse(
            habit.Id.Value,
            habit.Description,
            habit.FrequencyTimeUnit,
            habit.FrequencyCount.Value));
        return habitResponseList;
    }
}