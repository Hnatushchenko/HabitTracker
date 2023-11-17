using Application.BadHabits.AddOccurrence;
using Application.BadHabits.Create;
using Application.BadHabits.Delete;
using Application.BadHabits.DeleteOccurrence;
using Application.BadHabits.Get;
using Application.BadHabits.GetStatistic.GetBadHabitStatistic;
using Carter;
using Domain.BadHabit;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

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
            var successOrError = await sender.Send(addBadHabitOccurrenceCommand, cancellationToken);
            var actionResult = successOrError.Match(
                success => Results.NoContent(),
                error => Results.Conflict(new
                {
                    Message = "Cannot add duplicate occurrence of the bad habit. An occurrence with the specified date already exists.",
                    Details = new
                    {
                        BadHabitId = id,
                        request.OccurrenceDate
                    }
                })
            );
            return actionResult;
        });

        app.MapPatch("bad-habits/{id:guid}/remove-occurrence", async (Guid id,
            ISender sender,
            DeleteBadHabitOccurrenceRequest request,
            CancellationToken cancellationToken) =>
        {
            var deleteBadHabitOccurrenceCommand = new DeleteBadHabitOccurrenceCommand
            {
                OccurrenceDate = request.OccurrenceDate,
                BadHabitId = new BadHabitId(id)
            };
            await sender.Send(deleteBadHabitOccurrenceCommand, cancellationToken);
            return Results.NoContent();
        });

        app.MapDelete("bad-habits/{id:guid}", async (Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var deleteBadHabitCommand = new DeleteBadHabitCommand
            {
                BadHabitId = new BadHabitId(id)
            };
            await sender.Send(deleteBadHabitCommand, cancellationToken);
            return Results.NoContent();
        });
    }
}