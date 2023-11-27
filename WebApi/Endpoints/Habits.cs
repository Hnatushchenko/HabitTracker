using Application.Habits.Archive;
using Application.Habits.Create;
using Application.Habits.Delete;
using Application.Habits.Get;
using Application.Habits.Statistic.Get;
using Application.Habits.Update;
using Carter;
using Domain.Habit;
using MediatR;

namespace WebApi.Endpoints;

public sealed class Habits : ICarterModule
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
        
        app.MapPatch("habits/{id:guid}", async (Guid id, UpdateHabitDetailsRequest request, ISender sender) =>
        {
            var updateHabitDetailsCommand = new UpdateHabitDetailsCommand
            {
                HabitId = new HabitId(id),
                ToDoItemDescription = request.ToDoItemDescription,
                DefaultStartTime = request.DefaultStartTime,
                DefaultEndTime = request.DefaultEndTime,
                Description = request.Description
            };
            await sender.Send(updateHabitDetailsCommand);
            return Results.NoContent();
        });
        
        app.MapPost("habits", async (CreateHabitCommand command, ISender sender) =>
        {
            await sender.Send(command);
            return Results.Ok();
        });

        app.MapDelete("habits/{habitId:guid}", async (Guid habitId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var deleteHabitCommand = new DeleteHabitCommand(new HabitId(habitId));
            await sender.Send(deleteHabitCommand, cancellationToken);
            return Results.NoContent();
        });

        app.MapGet("habits/{id:guid}/statistic", async (Guid id, ISender sender) =>
        {
            var getHabitStatisticQuery = new GetHabitStatisticQuery(new HabitId(id));
            var getHabitStatisticResponse = await sender.Send(getHabitStatisticQuery);
            return Results.Ok(getHabitStatisticResponse);
        });
    }
}