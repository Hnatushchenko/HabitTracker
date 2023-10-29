using Application.Habits.Archive;
using Application.Habits.Create;
using Application.Habits.Delete;
using Application.Habits.Get;
using Application.Habits.Statistic.Get;
using Carter;
using Domain.Habit;
using MediatR;

namespace WebApi.Endpoints;

public class Habits : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("habits", async (ISender sender) =>
        {
            var habits = await sender.Send(new GetHabitsQuery());
            return Results.Ok(habits);
        });
        
        app.MapPatch("habits/{id:guid}", async (Guid id, ISender sender) =>
        {
            var archiveHabitCommand = new ArchiveHabitCommand(new HabitId(id));
            var updateOperationResult = await sender.Send(archiveHabitCommand);
            var result = updateOperationResult.Match(success => Results.NoContent(),
                notFound => Results.NotFound());
            return result;
        });
        
        app.MapPost("habits", async (CreateHabitCommand command, ISender sender) =>
        {
            await sender.Send(command);
            return Results.Ok();
        });

        app.MapDelete("habits/{id:guid}", async (Guid id, ISender sender) =>
        {
            var deleteHabitCommand = new DeleteHabitCommand(new HabitId(id));
            var deleteOperationResult = await sender.Send(deleteHabitCommand);
            var result = deleteOperationResult.Match(deleted => Results.NoContent(),
                notFound => Results.NotFound());
            return result;
        });

        app.MapGet("habits/{id:guid}/statistic", async (Guid id, ISender sender) =>
        {
            var getHabitStatisticQuery = new GetHabitStatisticQuery(new HabitId(id));
            var getHabitStatisticResponse = await sender.Send(getHabitStatisticQuery);
            return Results.Ok(getHabitStatisticResponse);
        });
    }
}