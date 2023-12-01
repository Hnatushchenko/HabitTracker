using Application.ToDoItems.Create;
using Application.ToDoItems.Delete;
using Application.ToDoItems.DueTomorrow;
using Application.ToDoItems.Get;
using Application.ToDoItems.HideForTheRestOfTheDay;
using Application.ToDoItems.Swap;
using Application.ToDoItems.Update.Details;
using Application.ToDoItems.Update.IsDone;
using Carter;
using Domain.ToDoItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Endpoints;

public sealed class ToDoItems : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        
        app.MapGet("to-do-items/{targetDate}", async ([FromRoute] DateTimeOffset targetDate,
            ISender sender, CancellationToken cancellationToken) =>
        {
            var getToDoItemsQuery = new GetToDoItemsQuery
            {
                TargetDate = targetDate
            };
            var toDoItems = await sender.Send(getToDoItemsQuery, cancellationToken);
            return Results.Ok(toDoItems);
        });
        
        app.MapGet("to-do-items", async (ISender sender,
            TimeProvider timeProvider, 
            CancellationToken cancellationToken) =>
        {
            var getToDoItemsQuery = new GetToDoItemsQuery
            {
                TargetDate = timeProvider.GetUtcNow()
            };
            var toDoItems = await sender.Send(getToDoItemsQuery, cancellationToken);
            return Results.Ok(toDoItems);
        });

        app.MapPatch("to-do-items/is-done/{toDoItemId:guid}", async (ToDoItemId toDoItemId,
            UpdateToDoItemIsDoneRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var updateToDoItemIdDoneCommand = new UpdateToDoItemIsDoneCommand(toDoItemId,
                request.NewIsDoneValue);
            await sender.Send(updateToDoItemIdDoneCommand, cancellationToken);
            return Results.NoContent();
        });
        
        app.MapPatch("to-do-items/details/{toDoItemId:guid}", async (ToDoItemId toDoItemId,
            UpdateToDoItemDetailsRequest request, 
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var updateToDoItemDetailsCommand = new UpdateToDoItemDetailsCommand(toDoItemId,
                request.StartTime, request.EndTime, request.Description);
            await sender.Send(updateToDoItemDetailsCommand, cancellationToken);
            return Results.NoContent();
        });

        app.MapPatch("to-do-items/{toDoItemId:guid}/due-tomorrow", async (ToDoItemId toDoItemId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var dueToDoItemTomorrowCommand = new DueToDoItemTomorrowCommand(toDoItemId);
            await sender.Send(dueToDoItemTomorrowCommand, cancellationToken);
            return Results.NoContent();
        });
        
        app.MapPatch("to-do-items/{toDoItemId:guid}/hide-for-the-rest-of-the-day", async (ToDoItemId toDoItemId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new HideToDoItemForTheRestOfTheDayCommand(toDoItemId);
            await sender.Send(command, cancellationToken);
            return Results.NoContent();
        });

        app.MapPost("to-do-items/swap", async (SwapToDoItemsRequest request, 
            CancellationToken cancellationToken,
            ISender sender) =>
        {
            var swapToDoItemsCommand = new SwapToDoItemsCommand
            {
                FirstToDoItemId = new ToDoItemId(request.FirstToDoItemId),
                SecondToDoItemId = new ToDoItemId(request.SecondToDoItemId)
            };
            await sender.Send(swapToDoItemsCommand, cancellationToken);
            return Results.NoContent();
        });
        
        app.MapPost("to-do-items", async (CreateToDoItemCommand command,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            await sender.Send(command, cancellationToken);
            return Results.NoContent();
        });

        app.MapDelete("to-do-items/{toDoItemId}", async (ToDoItemId toDoItemId,
            ISender sender, CancellationToken cancellationToken) =>
        {
            var deleteToDoItemCommand = new DeleteToDoItemCommand(toDoItemId);
            await sender.Send(deleteToDoItemCommand, cancellationToken);
            return Results.NoContent();
        });
    }
}