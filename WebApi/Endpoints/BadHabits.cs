using Application.BadHabits.Create;
using Application.BadHabits.Get;
using Application.BadHabits.GetStatistic.GetBadHabitStatistic;
using Carter;
using Domain.BadHabit;
using Domain.Habit;
using MediatR;

namespace WebApi.Endpoints;

public sealed class BadHabits : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("bad-habits", async (ISender sender) =>
        {
            var badHabitResponseList = await sender.Send(GetBadHabitsQuery.Instance);
            return Results.Ok(badHabitResponseList);
        });
        
        app.MapGet("bad-habits/{id:guid}/statistic", async (Guid id, ISender sender) =>
        {
            var getBadHabitStatisticQuery = new GetBadHabitStatisticQuery
            {
                BadHabitId = new BadHabitId(id)
            };
            var getBadHabitStatisticResponse = await sender.Send(getBadHabitStatisticQuery);
            return Results.Ok(getBadHabitStatisticResponse);
        });

        app.MapPost("bad-habits", async (ISender sender, CreateBadHabitCommand createBadHabitCommand) =>
        {
            await sender.Send(createBadHabitCommand);
            return Results.NoContent();
        });
    }
}