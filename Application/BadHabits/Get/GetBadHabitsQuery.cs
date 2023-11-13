using MediatR;

namespace Application.BadHabits.Get;

public sealed class GetBadHabitsQuery : IRequest<IEnumerable<BadHabitResponse>>
{
    private GetBadHabitsQuery() {}

    public static GetBadHabitsQuery Instance { get; } = new ();
}