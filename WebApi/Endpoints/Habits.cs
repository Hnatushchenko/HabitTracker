using Application.Habits.Archive;
using Application.Habits.Create;
using Application.Habits.Delete;
using Application.Habits.Get;
using Application.Habits.Statistic.Get;
using Application.Habits.Unarchive;
using Application.Habits.Update;
using Carter;
using Domain.Habit.ValueObjects;
using MediatR;

namespace WebApi.Endpoints;

public sealed class Habits : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("habits", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var habits = await sender.Send(new GetHabitsQuery(), cancellationToken);
            return Results.Ok(habits);
        });
        
        app.MapPatch("habits/{id:guid}/archive", async (Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var archiveHabitCommand = new ArchiveHabitCommand(new HabitId(id));
            var result = await sender.Send(archiveHabitCommand, cancellationToken);
            var actionResult = result.Match(success => Results.NoContent(), Results.BadRequest);
            return actionResult;
        });
        
        app.MapPatch("habits/{habitId:guid}/unarchive", async (Guid habitId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var unarchiveHabitCommand = new UnarchiveHabitCommand(new HabitId(habitId));
            var result = await sender.Send(unarchiveHabitCommand, cancellationToken);
            var actionResult = result.Match(success => Results.NoContent(), Results.BadRequest);
            return actionResult;
        });
        
        app.MapPatch("habits/{id:guid}", async (Guid id, UpdateHabitDetailsRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var updateHabitDetailsCommand = new UpdateHabitDetailsCommand
            {
                HabitId = new HabitId(id),
                ToDoItemDescription = request.ToDoItemDescription,
                DefaultStartTime = request.DefaultStartTime,
                DefaultEndTime = request.DefaultEndTime,
                Description = request.Description
            };
            await sender.Send(updateHabitDetailsCommand, cancellationToken);
            return Results.NoContent();
        });
        
        app.MapPost("habits", async (CreateHabitCommand command,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            await sender.Send(command, cancellationToken);
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

        app.MapGet("habits/{id:guid}/statistic", async (Guid id,
            ISender sender, CancellationToken cancellationToken) =>
        {
            var getHabitStatisticQuery = new GetHabitStatisticQuery(new HabitId(id));
            var getHabitStatisticResponse = await sender.Send(getHabitStatisticQuery, cancellationToken);
            return Results.Ok(getHabitStatisticResponse);
        });
    }
}