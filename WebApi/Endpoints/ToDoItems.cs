using Application.ToDoItems.Create;
using Application.ToDoItems.Delete;
using Application.ToDoItems.DueTomorrow;
using Application.ToDoItems.Get;
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
        
        app.MapGet("to-do-items/{targetDate}", async ([FromRoute] DateTimeOffset targetDate, ISender sender) =>
        {
            var getToDoItemsQuery = new GetToDoItemsQuery
            {
                TargetDate = targetDate
            };
            var toDoItems = await sender.Send(getToDoItemsQuery);
            return Results.Ok(toDoItems);
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

        app.MapPatch("to-do-items/is-done/{id:guid}", async (Guid id, UpdateToDoItemIsDoneRequest request, ISender sender) =>
        {
            var updateToDoItemIdDoneCommand = new UpdateToDoItemIsDoneCommand(new ToDoItemId(id),
                request.NewIsDoneValue);
            var updateOperationResult = await sender.Send(updateToDoItemIdDoneCommand);
            var result = updateOperationResult.Match(updated => Results.NoContent(),
                notFound => Results.NotFound());
            return result;
        });
        
        app.MapPatch("to-do-items/details/{id:guid}", async (Guid id, UpdateToDoItemDetailsRequest request, ISender sender) =>
        {
            var updateToDoItemDetailsCommand = new UpdateToDoItemDetailsCommand(new ToDoItemId(id),
                request.StartTime, request.EndTime, request.Description);
            var updateOperationResult = await sender.Send(updateToDoItemDetailsCommand);
            var result = updateOperationResult.Match(updated => Results.NoContent(),
                notFound => Results.NotFound());
            return result;
        });

        app.MapPatch("to-do-items/{id:guid}/due-tomorrow", async (Guid id, ISender sender) =>
        {
            var dueToDoItemTomorrowCommand = new DueToDoItemTomorrowCommand(new ToDoItemId(id));
            await sender.Send(dueToDoItemTomorrowCommand);
            return Results.NoContent();
        });
        
        app.MapPost("to-do-items", async (CreateToDoItemCommand command, ISender sender) =>
        {
            await sender.Send(command);
            return Results.Ok();
        });

        app.MapDelete("to-do-items/{id:guid}", async (Guid id, ISender sender) =>
        {
            var deleteToDoItemCommand = new DeleteToDoItemCommand(new ToDoItemId(id));
            var deleteOperationResult = await sender.Send(deleteToDoItemCommand);
            var result = deleteOperationResult.Match(deleted => Results.NoContent(),
                notFound => Results.NotFound());
            return result;
        });
    }
}