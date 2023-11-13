using Application.BadHabits.Create;
using Application.BadHabits.Get;
using Carter;
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

        app.MapPost("bad-habits", async (ISender sender, CreateBadHabitCommand createBadHabitCommand) =>
        {
            await sender.Send(createBadHabitCommand);
            return Results.NoContent();
        });
    }
}