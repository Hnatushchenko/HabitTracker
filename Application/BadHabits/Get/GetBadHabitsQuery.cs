using MediatR;

namespace Application.BadHabits.Get;

public sealed class GetBadHabitsQuery : IRequest<BadHabitsResponse>
{
    private GetBadHabitsQuery() {}

    public static GetBadHabitsQuery Instance { get; } = new ();
}