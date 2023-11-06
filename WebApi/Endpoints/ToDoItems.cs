using Application.Data;
using Application.ToDoItems.Create;
using Application.ToDoItems.Delete;
using Application.ToDoItems.DueTomorrow;
using Application.ToDoItems.Get;
using Application.ToDoItems.HideForTheRestOfTheDay;
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
        
        app.MapGet("to-do-items/{targetDate}", async (CancellationToken token, IApplicationContext applicationContext, [FromRoute] DateTimeOffset targetDate, ISender sender) =>
        {
            var getToDoItemsQuery = new GetToDoItemsQuery
            {
                TargetDate = targetDate
            };
            var toDoItems2 = await sender.Send(getToDoItemsQuery);
            return Results.Ok(toDoItems2);
        });
        
        app.MapGet("to-do-items", async (ISender sender) =>
        {
            var getToDoItemsQuery = new GetToDoItemsQuery
            {
                TargetDate = DateTimeOffset.Now
            };
            var toDoItems = await sender.Send(getToDoItemsQuery);
            return Results.Ok(toDoItems);
        });

        app.MapPatch("to-do-items/is-done/{toDoItemId:guid}", async (ToDoItemId toDoItemId, UpdateToDoItemIsDoneRequest request, ISender sender) =>
        {
            var updateToDoItemIdDoneCommand = new UpdateToDoItemIsDoneCommand(toDoItemId,
                request.NewIsDoneValue);
            var updateOperationResult = await sender.Send(updateToDoItemIdDoneCommand);
            var result = updateOperationResult.Match(updated => Results.NoContent(),
                notFound => Results.NotFound());
            return result;
        });
        
        app.MapPatch("to-do-items/details/{toDoItemId:guid}", async (ToDoItemId toDoItemId, UpdateToDoItemDetailsRequest request, ISender sender) =>
        {
            var updateToDoItemDetailsCommand = new UpdateToDoItemDetailsCommand(toDoItemId,
                request.StartTime, request.EndTime, request.Description);
            var updateOperationResult = await sender.Send(updateToDoItemDetailsCommand);
            var result = updateOperationResult.Match(updated => Results.NoContent(),
                notFound => Results.NotFound());
            return result;
        });

        app.MapPatch("to-do-items/{toDoItemId:guid}/due-tomorrow", async (ToDoItemId toDoItemId, ISender sender) =>
        {
            var dueToDoItemTomorrowCommand = new DueToDoItemTomorrowCommand(toDoItemId);
            await sender.Send(dueToDoItemTomorrowCommand);
            return Results.NoContent();
        });
        
        app.MapPatch("to-do-items/{toDoItemId:guid}/hide-for-the-rest-of-the-day", async (ToDoItemId toDoItemId, ISender sender) =>
        {
            var command = new HideToDoItemForTheRestOfTheDayCommand(toDoItemId);
            await sender.Send(command);
            return Results.NoContent();
        });
        
        app.MapPost("to-do-items", async (CreateToDoItemCommand command, ISender sender) =>
        {
            await sender.Send(command);
            return Results.Ok();
        });

        app.MapDelete("to-do-items/{toDoItemId}", async (ToDoItemId toDoItemId, ISender sender) =>
        {
            var deleteToDoItemCommand = new DeleteToDoItemCommand(toDoItemId);
            var deleteOperationResult = await sender.Send(deleteToDoItemCommand);
            var result = deleteOperationResult.Match(deleted => Results.NoContent(),
                notFound => Results.NotFound());
            return result;
        });
    }
}