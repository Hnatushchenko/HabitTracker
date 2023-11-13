using Application.BadHabits.AddOccurrence;
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
        app.MapGet("bad-habits", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var badHabitResponseList = await sender.Send(GetBadHabitsQuery.Instance, cancellationToken);
            return Results.Ok(badHabitResponseList);
        });
        
        app.MapGet("bad-habits/{id:guid}/statistic", async (Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var getBadHabitStatisticQuery = new GetBadHabitStatisticQuery
            {
                BadHabitId = new BadHabitId(id)
            };
            var getBadHabitStatisticResponse = await sender.Send(getBadHabitStatisticQuery, cancellationToken);
            return Results.Ok(getBadHabitStatisticResponse);
        });

        app.MapPost("bad-habits", async (ISender sender,
            CreateBadHabitCommand createBadHabitCommand,
            CancellationToken cancellationToken) =>
        {
            await sender.Send(createBadHabitCommand, cancellationToken);
            return Results.NoContent();
        });

        app.MapPatch("bad-habits/{id:guid}/add-occurrence", async (Guid id,
            ISender sender,
            AddBadHabitOccurrenceRequest request,
            CancellationToken cancellationToken) =>
        {
            var addBadHabitOccurrenceCommand = new AddBadHabitOccurrenceCommand
            {
                OccurrenceDate = request.OccurrenceDate,
                BadHabitId = new BadHabitId(id)
            };
            await sender.Send(addBadHabitOccurrenceCommand, cancellationToken);
            return Results.NoContent();
        });
    }
}