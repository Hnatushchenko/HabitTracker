using MediatR;

namespace Application.Habits.Get;

public sealed record GetHabitsQuery : IRequest<HabitsResponse>
{
    private GetHabitsQuery() {}

    public static GetHabitsQuery Instance { get; } = new();
}
